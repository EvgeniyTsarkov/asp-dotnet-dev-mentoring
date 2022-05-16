namespace NorthwindWebsite.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServicesConfiguration(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            return services;
        }
    }
}
