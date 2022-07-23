using Microsoft.AspNetCore.Localization;
using NorthwindWebsite.Core.ApplicationSettings;

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

        app.UseCors(x => x
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next();
        });

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(appSettings.Localization.Default)
        });

        app.UseOpenApi();

        app.UseSwaggerUi3();

        app.UseResponseCaching();

        app.UseImageCachingMiddleware();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapRazorPages();

        app.ConfigureRouting();
    }
}
