using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Core.ApplicationSettings
{
    public class EmailSenderConfigs : IValidatable
    {
        [Required]
        public string SendersEmail { get; set; }

        [Required]
        public string SendersName { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
