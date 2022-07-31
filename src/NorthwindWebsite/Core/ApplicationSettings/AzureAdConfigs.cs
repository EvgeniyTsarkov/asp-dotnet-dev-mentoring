using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class AzureAdConfigs : IValidatable
{
    [Required]
    public string Instance { get; set; }

    [Required]
    public string ClientId { get; set; }

    [Required]
    public string TenantId { get; set; }

    public string CallbackPath { get; set; }

    public string CookieSchemeName { get; set; }

    public void Validate()
    {
        Validator.ValidateObject(this, new ValidationContext(this), true);
    }
}
