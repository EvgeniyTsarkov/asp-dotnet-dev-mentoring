using Microsoft.AspNetCore.Mvc.Razor;
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

        services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add
                ("~/Presentation/Views/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add
                ("~/Presentation/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add
                ("~/Presentation/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
            });

        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IProductService, ProductService>();
    }
}
