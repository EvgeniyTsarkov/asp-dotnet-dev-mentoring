using NorthwindWebsite.Middleware;

namespace NorthwindWebsite.Configuration;

public static class ImageMiddlewareConfiguration
{
    private const string ImagesControllerRoute = "/images/";

    public static void UseImageCachingMiddleware(this WebApplication app)
    {
        app.MapWhen(context =>
            context.Request.Path.Value!.Contains(ImagesControllerRoute),
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
