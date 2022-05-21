using NetEscapades.Configuration.Validation;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class AppSettings : IValidatable
{
    public string AllowedHosts { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public LogLevel LogLevel { get; set; }

    public int MaximumProductsOnPage { get; set; }

    public void Validate()
    {
        this.ConnectionStrings.Validate();
    }
}
