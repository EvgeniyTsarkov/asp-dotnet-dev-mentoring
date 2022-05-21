using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categoriesList = await _categoryService.GetAll();

        var categories = categoriesList.ToList();

        return View("Index", categories);
    }
}
