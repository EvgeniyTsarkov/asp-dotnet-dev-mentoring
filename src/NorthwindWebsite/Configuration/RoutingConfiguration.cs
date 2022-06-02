namespace NorthwindWebsite.Configuration;

public static class RoutingConfiguration
{
    public static void ConfigureRouting(this WebApplication app)
    {
        app.UseEndpoints(endponts =>
        {
            endponts.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endponts.MapControllerRoute(
                name: "images",
                pattern: "Images/{id}",
                defaults: new { controller = "Categories", action = "Images" });
        });
    }
}
