using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Core.ApplicationSettings
{
    public class ConnectionStrings : IValidatable
    {
        [Required]
        public string Default { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
