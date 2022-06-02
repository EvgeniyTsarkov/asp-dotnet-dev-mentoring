using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.CustomExceptions.BusinessExceptions;
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

            var uploadedFileSize = value is IFormFile uploadedFile
                ? uploadedFile.Length
                : throw new NotAFileException("Object is not a file");

            if (uploadedFileSize > size)
            {
                return new ValidationResult(string.Format(ErrorMessagePattern, size / bytesInKilobyte));
            }

            return ValidationResult.Success;
        }
    }
}
