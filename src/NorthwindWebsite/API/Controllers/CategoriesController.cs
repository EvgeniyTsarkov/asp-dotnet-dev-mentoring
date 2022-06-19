using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet(Name = nameof(GetCategories))]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAll();

        return Ok(categories.ToList());
    }
}
