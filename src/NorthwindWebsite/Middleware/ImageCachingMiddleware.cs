using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Constants;
using NorthwindWebsite.Core.Utils;

namespace NorthwindWebsite.Middleware;

public class ImageCachingMiddleware : IMiddleware
{
    private const string ImagesUrlSubstring = "/images/";

    private readonly AppSettings _appSettings;
    private readonly IImageCachingService _imageCachingService;
    private readonly ILogger<ImageCachingMiddleware> _logger;

    public ImageCachingMiddleware(
        AppSettings appSettings,
        IImageCachingService cacheService,
        ILogger<ImageCachingMiddleware> logger)
    {
        _appSettings = appSettings;
        _imageCachingService = cacheService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        DateTime.TryParse(context.Response.Headers["expires"], out var expires);

        var requestPath = context.Request.Path.Value;

        string imageIndex = requestPath!.Contains(ImagesUrlSubstring)
            ? requestPath!.GetImageIndexFromRequest()
            : string.Empty;

        if (requestPath!.Contains(ImagesUrlSubstring)
            && _imageCachingService.IsContained(imageIndex))
        {
            var imageAsBytes = _imageCachingService.GetImageFromCache(Convert.ToInt32(imageIndex));

            using var stream = new MemoryStream(imageAsBytes);

            context.Response.ContentType = HttpContentConstants.ImageBmp;

            stream.Position = default;

            _logger.LogWarning("Getting image from cache");
            await stream.CopyToAsync(context.Response.Body);
        }
        else if (requestPath!.Contains(ImagesUrlSubstring))
        {
            if (expires.Subtract(DateTime.Now) <= TimeSpan.Zero)
            {
                _imageCachingService.DumpImageCache();
            }

            var fileSavingPath = string.Concat(
                AppDomain.CurrentDomain.BaseDirectory,
                _appSettings.CachingConfigs.CachingFolder,
                imageIndex, FileNameConstants.BmpExtension);

            var originalResponseStream = context.Response.Body;

            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            context.Response.GetTypedHeaders().Expires = 
                DateTime.Now.AddSeconds(_appSettings.CachingConfigs.CachingPeriod); 

            await next.Invoke(context);

            var numberOfFilesInCachingFolder = _imageCachingService.GetNumberOfFilesInCachingFolder();

            if (context.Response.ContentType != null
                && context.Response.ContentType.Contains(HttpContentConstants.ImageBmp)
                && numberOfFilesInCachingFolder < _appSettings.CachingConfigs.CacheSize)
            {
                memoryStream.Position = default;

                _imageCachingService.CreateFolderIfDoesNotExists();

                using var fs = new FileStream(fileSavingPath, FileMode.Create);
                memoryStream.WriteTo(fs);
            }

            memoryStream.Position = default;

            await memoryStream.CopyToAsync(originalResponseStream);

            context.Response.Body = originalResponseStream;
        }
        else
        {
            await next.Invoke(context);
        }
    }
}
