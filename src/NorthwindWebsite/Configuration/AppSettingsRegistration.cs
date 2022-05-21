using NorthwindWebsite.Core.ApplicationSettings;

namespace NorthwindWebsite.Configuration
{
    public static class AppSettingsRegistration
    {
        public static void AddApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettings = GetAppSettings(configuration);

            services.AddSingleton(appSettings);
        }

        private static AppSettings GetAppSettings(IConfiguration configuration) =>
            new()
            {
                AllowedHosts = configuration.GetValue<string>("AllowedHosts"),
                ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
                LogLevel = configuration.GetSection("LogLevel").Get<Core.ApplicationSettings.LogLevel>(),
                MaximumProductsOnPage = configuration.GetValue<int>("MaximumProductsOnPage")
            };
    }
}
