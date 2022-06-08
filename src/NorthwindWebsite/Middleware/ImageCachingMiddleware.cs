using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Constants;
using NorthwindWebsite.Core.Utils;
using NorthwindWebsite.Middleware.Handlers.Interfaces;

namespace NorthwindWebsite.Middleware;

public class ImageCachingMiddleware : IMiddleware
{
    private readonly AppSettings _appSettings;
    private readonly IImageCachingHandler _imageCachingService;
    private readonly ILogger<ImageCachingMiddleware> _logger;

    public ImageCachingMiddleware(
        AppSettings appSettings,
        IImageCachingHandler cacheService,
        ILogger<ImageCachingMiddleware> logger)
    {
        _appSettings = appSettings;
        _imageCachingService = cacheService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var expiresParsed = DateTime.TryParse(context.Response.Headers["expires"], out var expires);

        var requestPath = context.Request.Path.Value;

        string imageIndex = requestPath!.GetImageIndexFromRequest();

        _imageCachingService.CreateFolderIfDoesNotExists();

        if (_imageCachingService.IsContained(imageIndex))
        {
            var indexParsed = int.TryParse(imageIndex, out var index);

            if (indexParsed)
            {
                _logger.LogWarning("Getting image from cache");

                await GetImageFromCache(context, index);
            }
        }
        else
        {
            if (expiresParsed && expires.Subtract(DateTime.Now) <= TimeSpan.Zero)
            {
                _imageCachingService.DumpImageCache();
            }

            await GetImageFromResponseAndCacheIt(context, next, imageIndex);
        }
    }

    private async Task GetImageFromCache(HttpContext context, int index)
    {
        var imageAsBytes = _imageCachingService.GetImageFromCache(index);

        using var stream = new MemoryStream(imageAsBytes);

        context.Response.ContentType = HttpContentConstants.ImageBmp;

        await stream.CopyToAsync(context.Response.Body);
    }

    private async Task GetImageFromResponseAndCacheIt(
            HttpContext context,
            RequestDelegate next,
            string imageIndex)
    {
        var fileSavingPath = BuildFileCachingPath(imageIndex);

        var originalResponseStream = context.Response.Body;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        context.Response.GetTypedHeaders().Expires =
            DateTime.Now.AddSeconds(_appSettings.CachingConfigs.CachingPeriod);

        await next.Invoke(context);

        var numberOfFilesInCachingFolder = _imageCachingService.GetNumberOfFilesInCachingFolder();

        var contentType = context.Response.ContentType;

        if (contentType != null
            && contentType.Contains(HttpContentConstants.ImageBmp)
            && numberOfFilesInCachingFolder < _appSettings.CachingConfigs.CacheSize)
        {
            memoryStream.Position = default;

            using var fs = new FileStream(fileSavingPath, FileMode.Create);
            memoryStream.WriteTo(fs);
        }

        memoryStream.Position = default;

        await memoryStream.CopyToAsync(originalResponseStream);

        context.Response.Body = originalResponseStream;
    }

    private string BuildFileCachingPath(string imageIndex) =>
        string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            _appSettings.CachingConfigs.CachingFolder,
            imageIndex, FileNameConstants.BmpExtension);
}
