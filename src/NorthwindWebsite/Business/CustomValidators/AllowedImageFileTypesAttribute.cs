using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Constants;
using NorthwindWebsite.Core.Utils;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Business.CustomValidators;

public class AllowedImageFileTypesAttribute : ValidationAttribute
{
    private string[] permittedExtentions;

    private string GetFileTypesRepresentation()
    {
        var joinedExtensions = string.Empty;

        foreach (var extension in permittedExtentions)
        {
            joinedExtensions = string.Join(", ", permittedExtentions);
        }

        return joinedExtensions;
    }

    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        var appSettings = validationContext.GetService<AppSettings>();

        permittedExtentions = appSettings.FileUploadOptions.ImageFileFormats;

        string contentType = string.Empty;

        if (value is IFormFile uploadedFile)
        {
            contentType = uploadedFile.GetContentType();
        }
        else
        {
            return new ValidationResult(AttributeErrorMessages.NotAFileErrorMessage);
        }

        if (!permittedExtentions.Contains(contentType))
        {
            return new ValidationResult(string.Format("Wrong file format. Please use {0}", GetFileTypesRepresentation()));
        }

        return ValidationResult.Success;
    }
}
