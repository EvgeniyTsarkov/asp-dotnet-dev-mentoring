using NorthwindWebsite.Core.ApplicationSettings;
using Serilog;

namespace NorthwindWebsite.Configuration
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogger(AppSettings appSettings)
        {
            var writingToFileConfigs = appSettings.Serilog.WriteTo
                .Single(writeTo => writeTo.Name == "File");

            var filePath = string.Format(writingToFileConfigs.Path,
                DateTime.Today.ToString("yyyy-MM-dd"));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(filePath,
                appSettings.Serilog.MinimumLevel.Default,
                writingToFileConfigs.Args.OutputTemplate)
                .CreateLogger();
        }
    }
}
