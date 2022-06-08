using NorthwindWebsite.Core.Constants;
using NorthwindWebsite.Middleware;

namespace NorthwindWebsite.Configuration;

public static class ImageMiddlewareConfiguration
{
    public static void UseImageCachingMiddleware(this WebApplication app)
    {
        app.MapWhen(context =>
        context.Request.Path.Value!.Contains(HttpContentConstants.ImagesControllerRoute),
        app =>
        {
            app.UseMiddleware<ImageCachingMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endponts =>
            {
                endponts.MapControllerRoute(
                    name: "images",
                    pattern: "images/{id}",
                    defaults: new { controller = "Categories", action = "Images" });
            });
        });
    }
}
