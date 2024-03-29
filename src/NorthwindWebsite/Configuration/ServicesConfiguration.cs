﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using NorthwindWebsite.Business.Services.Implementations;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.EmailSender;
using NorthwindWebsite.Filters;
using NorthwindWebsite.Infrastructure;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Implementation;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;
using NorthwindWebsite.Middleware;
using NorthwindWebsite.Middleware.Handlers.Implementations;
using NorthwindWebsite.Middleware.Handlers.Interfaces;
using NorthwindWebsite.Services.Implementations;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Configuration;

public static class ServicesConfiguration
{
    public static void AddServicesConfiguration(
        this IServiceCollection services,
        AppSettings appSettings)
    {
        services.AddControllersWithViews(options =>
            options.Filters.Add<LoggingFilter>());

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

        services.AddTransient<IImageCachingHandler, ImageCachingHandler>();

        services.AddTransient<ImageCachingMiddleware>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        services.AddSingleton<IEmailSender, EmailSender>();

        services.AddResponseCaching();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<NorthwindContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "MyAuth.Cookie";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
        services.AddHttpContextAccessor();
        services.AddScoped<SignInManager<ApplicationUser>>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            options.DefaultSignOutScheme = IdentityConstants.TwoFactorUserIdScheme;
        })
            .AddCookie(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.TwoFactorUserIdScheme)
            .AddExternalCookie();

        services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
        {
            microsoftOptions.ClientId = appSettings.MicrosoftAccountConfig.ClientId;
            microsoftOptions.ClientSecret = appSettings.MicrosoftAccountConfig.ClientSecret;
        })
            .AddAzureAD(options =>
        {
            options.Instance = appSettings.AzureAdConfigs.Instance;
            options.ClientId = appSettings.AzureAdConfigs.ClientId;
            options.TenantId = appSettings.AzureAdConfigs.TenantId;
            options.CallbackPath = appSettings.AzureAdConfigs.CallbackPath;
            options.CookieSchemeName = appSettings.AzureAdConfigs.CookieSchemeName;
        }).AddCookie();

        services
            .AddRazorPages()
            .AddRazorRuntimeCompilation();

        services.AddSwaggerDocument();
    }
}
