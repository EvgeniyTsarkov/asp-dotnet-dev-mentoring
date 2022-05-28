using NetEscapades.Configuration.Validation;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class AppSettings : IValidatable
{
    public string AllowedHosts { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public int MaximumProductsOnPage { get; set; }

    public Localization Localization { get; set; }

    public Serilog Serilog { get; set; }

    public AppSettings GetAppSettings(IConfiguration configuration) =>
        new()
        {
            AllowedHosts = configuration.GetValue<string>(nameof(AllowedHosts)),
            ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
            MaximumProductsOnPage = configuration.GetValue<int>(nameof(MaximumProductsOnPage)),
            Localization = configuration.GetSection(nameof(Localization)).Get<Localization>(),
            Serilog = configuration.GetSection(nameof(Serilog)).Get<Serilog>()
        };

    public void Validate()
    {
        ConnectionStrings.Validate();
        Serilog.Validate();
    }
}
