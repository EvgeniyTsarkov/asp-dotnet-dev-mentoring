using NorthwindWebsite.Services.Implementations;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Configuration;

public static class ServicesConfiguration
{
    public static void AddServicesConfiguration(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddDbContext<NorthwindContext>();
        services.AddTransient<ICategoryService, CategoryService>();
    }
}
