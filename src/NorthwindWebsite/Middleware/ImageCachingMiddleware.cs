using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Constants;
using NorthwindWebsite.Middleware.Handlers.Interfaces;
using NorthwindWebsite.Presentation.Utils;

namespace NorthwindWebsite.Middleware;

public class ImageCachingMiddleware : IMiddleware
{
    private readonly AppSettings _appSettings;
    private readonly IImageCachingHandler _imageCachingHandler;
    private readonly ILogger<ImageCachingMiddleware> _logger;

    public ImageCachingMiddleware(
        AppSettings appSettings,
        IImageCachingHandler cacheService,
        ILogger<ImageCachingMiddleware> logger)
    {
        _appSettings = appSettings;
        _imageCachingHandler = cacheService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var isExpiresValueParsed = DateTime.TryParse(context.Response.Headers["expires"], out var expires);

        var requestPath = context.Request.Path.Value;

        var imageIndex = UrlUtils.GetImageIndexFromRequest(requestPath);

        ValidateCachingDerectoryExistence();

        if (isExpiresValueParsed && expires.Subtract(DateTime.Now) <= TimeSpan.Zero)
        {
            _imageCachingHandler.DumpImageCache();
        }

        if (_imageCachingHandler.IsContained(imageIndex))
        {
            var isImageIndexParsed = int.TryParse(imageIndex, out var index);

            if (isImageIndexParsed)
            {
                _logger.LogWarning("Getting image from cache");

                await GetImageFromCache(context, index);
            }
        }
        else
        {
            await GetImageFromResponseAndCacheIt(context, next, imageIndex);
        }
    }

    private async Task GetImageFromCache(HttpContext context, int index)
    {
        var imageAsBytes = _imageCachingHandler.GetImageFromCache(index);

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

        var numberOfFilesInCachingFolder = _imageCachingHandler.GetNumberOfFilesInCachingFolder();

        var contentType = context.Response.ContentType;

        var contentTypeIsNotNull = contentType != null;

        if (contentTypeIsNotNull
            && contentType!.Contains(HttpContentConstants.ImageBmp)
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

    private void ValidateCachingDerectoryExistence()
    {
        var doesDirectoryExist = _imageCachingHandler.DoesCachingDirectoryExist();

        if (!doesDirectoryExist)
        {
            _imageCachingHandler.CreateCachingFolder();
        }
    }
}
