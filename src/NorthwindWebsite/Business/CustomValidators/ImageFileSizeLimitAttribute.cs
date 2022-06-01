using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Core.ApplicationSettings;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Business.CustomValidators
{
    public class ImageFileSizeLimitAttribute : ValidationAttribute
    {
        private int bytesInKilobyte = 1000;

        private int size;

        public string GetErrorMessage() =>
            string.Format("The file is too large. Maximum file size is {0} kB.", size / bytesInKilobyte);

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var fileUploadModel = (FileUploadDto)validationContext.ObjectInstance;

            var appsettings = validationContext.GetService<AppSettings>();

            size = appsettings!.FileUploadOptions.ImageMaxSize;

            if (fileUploadModel.FileUpload.Length > size)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
