using NorthwindWebsite.Core.ApplicationSettings;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Business.CustomValidators
{
    public class ImageFileSizeLimitAttribute : ValidationAttribute
    {
        private const int bytesInKilobyte = 1000;

        private int size;

        public string GetErrorMessage() =>
            string.Format("The file is too large. Maximum file size is {0} kB.", size / bytesInKilobyte);

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var appsettings = validationContext.GetService<AppSettings>();

            var uploadedFile = (IFormFile)value;

            size = appsettings!.FileUploadOptions.ImageMaxSize;

            if (uploadedFile.Length > size)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
