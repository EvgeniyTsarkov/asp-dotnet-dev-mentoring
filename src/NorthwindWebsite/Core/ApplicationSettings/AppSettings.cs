using NetEscapades.Configuration.Validation;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class AppSettings : IValidatable
{
    public string AllowedHosts { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public int MaximumProductsOnPage { get; set; }

    public Localization Localization { get; set; }

    public SerilogConfig SerilogConfiguration { get; set; }

    public FileUploadOptions FileUploadOptions { get; set; }

    public AppSettings GetAppSettings(IConfiguration configuration) =>
        new()
        {
            AllowedHosts = configuration.GetValue<string>(nameof(AllowedHosts)),
            ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
            MaximumProductsOnPage = configuration.GetValue<int>(nameof(MaximumProductsOnPage)),
            Localization = configuration.GetSection(nameof(Localization)).Get<Localization>(),
            SerilogConfiguration = configuration.GetSection(nameof(Serilog)).Get<SerilogConfig>(),
            FileUploadOptions = configuration.GetSection(nameof(FileUploadOptions)).Get<FileUploadOptions>(),
        };

    public void Validate()
    {
        ConnectionStrings.Validate();
        SerilogConfiguration.Validate();
    }
}
