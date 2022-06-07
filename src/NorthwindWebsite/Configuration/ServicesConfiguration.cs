using Microsoft.AspNetCore.Mvc.Razor;
using NorthwindWebsite.Business.Services.Implementations;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Infrastructure.Repositories.Implementation;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;
using NorthwindWebsite.Middleware;
using NorthwindWebsite.Services.Implementations;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Configuration;

public static class ServicesConfiguration
{
    public static void AddServicesConfiguration(
        this IServiceCollection services, AppSettings appSettings)
    {
        services.AddControllersWithViews();

        services.AddDbContextConfiguration(appSettings.ConnectionStrings.Default);

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
        services.AddTransient<ISupplierService, SupplierService>();

        services.AddTransient<IImageCachingService, ImageCachingService>();

        services.AddTransient<ImageCachingMiddleware>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        services.AddResponseCaching();
    }
}
