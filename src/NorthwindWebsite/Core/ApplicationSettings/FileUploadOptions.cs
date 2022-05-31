using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Core.ApplicationSettings
{
    public class FileUploadOptions : IValidatable
    {
        [Required]
        public string CategoryPictureFileFormats { get; set; }

        [Required]
        public int CategoryPicturesMaxSize { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
