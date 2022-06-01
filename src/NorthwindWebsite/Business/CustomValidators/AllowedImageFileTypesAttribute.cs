using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Core.Utils;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Business.CustomValidators;

public class AllowedImageFileTypesAttribute : ValidationAttribute
{
    private string[] permittedExtentions;

    public string GetErrorMessage() =>
        string.Format("Wrong file format. Please use {0}", GetFileTypesRepresentation());

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

        var contentType = value is IFormFile uploadedFile
            ? uploadedFile.GetContentType()
            : string.Empty;

        if (!permittedExtentions.Contains(contentType))
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }
}
