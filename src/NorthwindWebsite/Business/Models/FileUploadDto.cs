using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Business.Models;

public class FileUploadDto
{
    public int CategoryId { get; set; }

    public int MaximumFileSize { get; set; }

    [Required]
    [BindProperty]
    [Display(Name = "Select file to upload")]
    public IFormFile FileUpload { get; set; }
}
