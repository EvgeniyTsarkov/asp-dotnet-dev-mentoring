using NetEscapades.Configuration.Validation;
using NorthwindWebsite.Core.ApplicationSettings.SerilogFiles;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Core.ApplicationSettings
{
    public class SerilogConfig : IValidatable
    {
        public MinimumLevel MinimumLevel { get; set; }

        public bool ActionsLogging { get; set; }

        [Required]
        public List<WriteTo> WriteTo { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
