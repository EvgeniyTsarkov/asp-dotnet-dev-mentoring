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

    /// <summary>
    /// Returns all categories from database.
    /// </summary>
    /// <returns><see cref="Task">Representing an asynchronous operarion.</returns>
    [HttpGet(Name = nameof(GetCategories))]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        var categories = await _categoryService.GetAll();

        return categories.ToList();
    }

    /// <summary>
    /// Returns and image for a category by id.
    /// </summary>
    /// <param name="categoryId">Id of the category that contains the image.</param>
    /// <response code="404">Unable to find a category with the mentioned id.</response>
    /// <returns><see cref="Task">Represents an asynchronous operation.</returns>
    [HttpGet("id/{categoryId:int}/image")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetImage(int categoryId)
    {
        var picture = await _categoryService.GetImage(categoryId);

        if (picture == null)
        {
            return NotFound();
        }

        return File(picture, FileFormat);
    }

    /// <summary>
    /// Saves mthe image to the database for the category with the specified id.
    /// </summary>
    /// <param name="categoryId">Id of the category the image will be saved to.</param>
    /// <param name="imageDto"><see cref="ImageDto"></param>
    /// <response code="404">Unable to find a category with the mentioned id.</response>
    /// <returns><see cref="Task">Represents an asynchronous operation.</returns>
    [HttpPut("id/{categoryId:int}/image")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
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
