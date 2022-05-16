namespace NorthwindWebsite.Configuration;

public static class ServicesConfiguration
{
    public static void AddServicesConfiguration(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
    }
}
