using NetEscapades.Configuration.Validation;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class AppSettings : IValidatable
{
    public string AllowedHosts { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public LogLevel LogLevel { get; set; }

    public int MaximumProductsOnPage { get; set; }

    public AppSettings GetAppSettings(IConfiguration configuration) =>
        new()
        {
            AllowedHosts = configuration.GetValue<string>("AllowedHosts"),
            ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
            LogLevel = configuration.GetSection("LogLevel").Get<Core.ApplicationSettings.LogLevel>(),
            MaximumProductsOnPage = configuration.GetValue<int>("MaximumProductsOnPage")
        };

    public void Validate()
    {
        this.ConnectionStrings.Validate();
    }
}
