using NorthwindWebsite.Core.ApplicationSettings;
using Serilog;

namespace NorthwindWebsite.Configuration
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogger(
            this WebApplicationBuilder builder, 
            AppSettings appSettings)
        {
            var writingToFileConfigs = appSettings.SerilogConfiguration.WriteTo
                .Single(writeTo => writeTo.Name == "File");

            var filePath = string.Format(writingToFileConfigs.Path,
                DateTime.Today.ToString("yyyy-MM-dd"));

            builder.Host.UseSerilog((ctx, lc) => lc
            .Enrich.FromLogContext()
            .WriteTo.File(string.Concat(AppDomain.CurrentDomain.BaseDirectory, filePath),
            appSettings.SerilogConfiguration.MinimumLevel.Default,
            writingToFileConfigs.Args.OutputTemplate));
        }
    }
}
