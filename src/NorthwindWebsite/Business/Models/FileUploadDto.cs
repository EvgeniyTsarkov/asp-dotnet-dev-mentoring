using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Business.Models;

public class FileUploadDto
{
    public int CategoryId { get; set; }

    [Required]
    [BindProperty]
    [MaximumFileSize]
    [AllowedImageFileTypes]
    [Display(Name = "Select file to upload")]
    public IFormFile FileUpload { get; set; }
}
