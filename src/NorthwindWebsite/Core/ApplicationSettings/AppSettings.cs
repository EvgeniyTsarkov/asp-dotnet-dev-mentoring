﻿using NetEscapades.Configuration.Validation;
using NorthwindWebsite.Core.ApplicationSettings.CachingFiles;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class AppSettings : IValidatable
{
    public string AllowedHosts { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public int MaximumProductsOnPage { get; set; }

    public Localization Localization { get; set; }

    public SerilogConfig SerilogConfiguration { get; set; }

    public FileUploadOptions FileUploadOptions { get; set; }

    public CachingConfigs CachingConfigs { get; set; }

    public EmailSenderConfigs EmailSenderConfigs { get; set; }

    public MicrosoftAccountConfig MicrosoftAccountConfig { get; set; }

    public AzureAdConfigs AzureAdConfigs { get; set; }

    public AzureConnectionStringData AzureConnectionStringData { get; set; }

    private const string AzureClient = "Authentication:Microsoft:ClientId";

    public AppSettings GetAppSettings(IConfiguration configuration) =>
        new()
        {
            AllowedHosts = configuration.GetValue<string>(nameof(AllowedHosts)),
            ConnectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
            MaximumProductsOnPage = configuration.GetValue<int>(nameof(MaximumProductsOnPage)),
            Localization = configuration.GetSection(nameof(Localization)).Get<Localization>(),
            SerilogConfiguration = configuration.GetSection(nameof(Serilog)).Get<SerilogConfig>(),
            FileUploadOptions = configuration.GetSection(nameof(FileUploadOptions)).Get<FileUploadOptions>(),
            CachingConfigs = configuration.GetSection(nameof(CachingConfigs)).Get<CachingConfigs>(),
            EmailSenderConfigs = configuration.GetSection(nameof(EmailSenderConfigs)).Get<EmailSenderConfigs>(),
            MicrosoftAccountConfig = new MicrosoftAccountConfig
            {
                ClientId = configuration[AzureClient],
                ClientSecret = configuration["Authentication:Microsoft:ClientSecret"]
            },
            AzureAdConfigs = new AzureAdConfigs
            {
                Instance = configuration.GetValue<string>("AzureAdConfigs:Instance"),
                ClientId = configuration[AzureClient],
                TenantId = configuration.GetValue<string>("Authentication:AzureId:TenantId"),
                CallbackPath = configuration.GetValue<string>("AzureAdConfigs:CallbackPath"),
                CookieSchemeName = configuration.GetValue<string>("AzureAdConfigs:CookieSchemeName")
            }, 
            AzureConnectionStringData = new AzureConnectionStringData 
            {
                AzureConnectionStringId = configuration.GetValue<string>("AzureConnectionStringId"),
                AzureConnectionStringPassword = configuration.GetValue<string>("AzureConnectionStringPassword")
            }
        };

    public void Validate()
    {
        ConnectionStrings.Validate();
        SerilogConfiguration.Validate();
        EmailSenderConfigs.Validate();
        MicrosoftAccountConfig.Validate();
        AzureAdConfigs.Validate();
    }
}
