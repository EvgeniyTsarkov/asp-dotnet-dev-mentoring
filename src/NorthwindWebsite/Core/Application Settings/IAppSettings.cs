namespace NorthwindWebsite.Core.Application_Settings
{
    public interface IAppSettings
    {
        AppSettings ReadAppSettings(IConfiguration configuration);
    }
}
