using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Core.ApplicationSettings;

public class MicrosoftAccountConfig : IValidatable
{
    [Required]
    public string ClientId { get; set; }

    [Required]
    public string ClientSecret { get; set; }

    public void Validate()
    {
        Validator.ValidateObject(this, new ValidationContext(this), true);
    }
}
