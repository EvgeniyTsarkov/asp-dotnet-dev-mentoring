using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Business.CustomValidators
{
    public class ImageFileSizeLimitAttribute : ValidationAttribute
    {
        private const int bytesInKilobyte = 1000;

        private const string ErrorMessagePattern = "The file is too large. Maximum file size is {0} kB.";

        private int size;

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var appsettings = validationContext.GetService<AppSettings>();

            size = appsettings!.FileUploadOptions.ImageMaxSize;

            long uploadedFileSize = default;
                
            if(value is IFormFile uploadedFile) 
            {
                uploadedFileSize = uploadedFile.Length;
            }
            else 
            {
                return new ValidationResult(AttributeErrorMessages.NotAFileErrorMessage);
            }

            if (uploadedFileSize > size)
            {
                return new ValidationResult(string.Format(ErrorMessagePattern, size / bytesInKilobyte));
            }

            return ValidationResult.Success;
        }
    }
}
