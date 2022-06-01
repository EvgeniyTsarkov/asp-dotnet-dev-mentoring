using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthwindWebsite.Business.CustomValidators
{
    public class AllowedImageFileTypesAttribute : ValidationAttribute
    {
        private string[] permittedExtentions;

        public string GetErrorMessage() =>
            string.Format("Wrong file format. Please use{0}", GetFileTypesRepresentation());

        private StringBuilder GetFileTypesRepresentation()
        {
            var resultingString = new StringBuilder();

            foreach (var extension in permittedExtentions)
            {
                resultingString.Append(" " + extension + ",");
            }

            return resultingString;
        }

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var fileUploadModel = (FileUploadDto)validationContext.ObjectInstance;

            var appSettings = validationContext.GetService<AppSettings>();

            permittedExtentions = appSettings.FileUploadOptions.ImageFileFormats;

            var contentType = fileUploadModel.FileUpload.GetContentType();

            if (!permittedExtentions.Contains(contentType))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
