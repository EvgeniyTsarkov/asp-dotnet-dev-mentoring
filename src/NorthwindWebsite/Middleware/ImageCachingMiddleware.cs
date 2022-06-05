using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Utils;

namespace NorthwindWebsite.Middleware;

public class ImageCachingMiddleware : IMiddleware
{
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
        if (CacheTimer.Value.Subtract(DateTime.Now) < TimeSpan.Zero)
        {
            _imageCachingService.DumpImageCache();
        }

        var requestPath = context.Request.Path.Value;

        var imageIndex = requestPath!.GetImageIndexFromRequest();

        if (
            requestPath!.Contains("/images/") 
            && _imageCachingService.IsContained(imageIndex)) 
        {
            CacheTimer.Value = DateTime.Now.AddSeconds(_appSettings.CachingConfigs.CachingPeriod);

            var imageAsBytes = _imageCachingService.GetImageFromCache(Convert.ToInt32(imageIndex));

            var stream = new MemoryStream(imageAsBytes);

            context.Response.ContentType = "image/bmp";

            stream.Position = default;

            _logger.LogWarning("Getting image from cache");
            await stream.CopyToAsync(context.Response.Body);
        }
        else if (requestPath!.Contains("/images/"))
        {
            CacheTimer.Value = DateTime.Now.AddSeconds(_appSettings.CachingConfigs.CachingPeriod);

            var fileSavingPath = string.Concat(
                AppDomain.CurrentDomain.BaseDirectory,
                _appSettings.CachingConfigs.CachingFolder,
                imageIndex, ".bmp");

            var originalResponseStream = context.Response.Body;

            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await next.Invoke(context);

            var numberOfFilesInCachingFolder = _imageCachingService.GetNumberOfFilesInCachingFolder();

            if (context.Response.ContentType != null
                && context.Response.ContentType.Contains("image/bmp")
                && numberOfFilesInCachingFolder < _appSettings.CachingConfigs.CacheSize)
            {
                memoryStream.Position = default;

                _imageCachingService.CreateFolderIfDoesNotExists();

                if (!_imageCachingService.IsContained(imageIndex))
                {
                    using var fs = new FileStream(fileSavingPath, FileMode.Create);
                    memoryStream.WriteTo(fs);
                }
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
