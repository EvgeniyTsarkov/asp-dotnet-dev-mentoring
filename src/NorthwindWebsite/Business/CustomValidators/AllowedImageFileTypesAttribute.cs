using NorthwindWebsite.Core.ApplicationSettings;
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
            contentType = uploadedFile.ContentType;
        }
        else
        {
            return new ValidationResult("Validation error: object is not a file");
        }

        if (!permittedExtentions.Contains(contentType))
        {
            return new ValidationResult(string.Format("Wrong file format. Please use {0}", GetFileTypesRepresentation()));
        }

        return ValidationResult.Success;
    }
}
