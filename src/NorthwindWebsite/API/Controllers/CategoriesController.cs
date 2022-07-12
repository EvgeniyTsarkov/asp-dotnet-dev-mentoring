using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private const string FileFormat = "image/bmp";

    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet(Name = nameof(GetCategories))]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        var categories = await _categoryService.GetAll();

        return categories.ToList();
    }

    [HttpGet("id/{categoryId:int}/image")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetImage(int categoryId)
    {
        var picture = await _categoryService.GetImage(categoryId);

        if (picture == null)
        {
            return NotFound();
        }

        return File(picture, FileFormat);
    }


    [HttpPut("id/{categoryId:int}/image")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UploadImage(
        int categoryId,
        [FromBody] ImageDto imageDto)
    {
        var category = await _categoryService.Get(categoryId);

        if (category == null)
        {
            return NotFound("Unable to find category by the provided id.");
        }

        category.Picture = Convert.FromBase64String(imageDto.Image);

        await _categoryService.Update(category);

        return File(category.Picture, FileFormat);
    }
}
