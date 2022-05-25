using NetEscapades.Configuration.Validation;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class AppSettings : IValidatable
{
    public string AllowedHosts { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public LogLevel LogLevel { get; set; }

    public int MaximumProductsOnPage { get; set; }

    public Localization Localization { get; set; }

    public AppSettings GetAppSettings(IConfiguration configuration) =>
        new()
        {
            AllowedHosts = configuration.GetValue<string>(nameof(AllowedHosts)),
            ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
            LogLevel = configuration.GetSection(nameof(LogLevel)).Get<LogLevel>(),
            MaximumProductsOnPage = configuration.GetValue<int>(nameof(MaximumProductsOnPage)),
            Localization = configuration.GetSection(nameof(Localization)).Get<Localization>()
        };

    public void Validate()
    {
        this.ConnectionStrings.Validate();
    }
}
