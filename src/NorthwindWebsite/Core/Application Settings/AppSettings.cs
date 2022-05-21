using NetEscapades.Configuration.Validation;

namespace NorthwindWebsite.Core.Application_Settings;

public class AppSettings : IAppSettings, IValidatable
{
    public string AllowedHosts { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public LogLevel LogLevel { get; set; }

    public string MaximumProductsOnPage { get; set; }

    public AppSettings ReadAppSettings(IConfiguration configuration) =>
        new AppSettings()
        {
            AllowedHosts = configuration.GetValue<string>(nameof(AllowedHosts)),
            ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
            LogLevel = configuration.GetSection(nameof(LogLevel)).Get<LogLevel>(),
            MaximumProductsOnPage = configuration.GetValue<string>(nameof(MaximumProductsOnPage))
        };

    public void Validate()
    {
        this.ConnectionStrings.Validate();
    }
}
