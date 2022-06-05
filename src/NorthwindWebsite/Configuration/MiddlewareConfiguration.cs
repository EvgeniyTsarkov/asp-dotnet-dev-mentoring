using Microsoft.AspNetCore.Localization;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Middleware;

namespace NorthwindWebsite.Configuration;

public static class MiddlewareConfiguration
{
    public static void AddMiddlewareConfiguration(
        this WebApplication app, AppSettings appSettings)
    {
        //Configure the HTTP request pipeline.

        if (!app.Environment.IsDevelopment())
        {
            app.ConfigureErrorHandling();

            // The default HSTS value is 30 days. You may want to change this for production scenarios,
            // see https://aka.ms/aspnetcore-hsts.

            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(appSettings.Localization.Default)
        });

        app.UseMiddleware<ImageCachingMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.ConfigureRouting();
    }
}
