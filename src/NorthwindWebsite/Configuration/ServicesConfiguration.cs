using NorthwindWebsite.Business.Services.Implementations;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Services.Implementations;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Configuration;

public static class ServicesConfiguration
{
    public static void AddServicesConfiguration(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IProductService, ProductService>();
    }
}
