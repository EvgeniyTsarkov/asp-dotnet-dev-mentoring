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
        var categories = await _categoryService.GetAll();

        var categoryNames = categories
            .Select(category => category.CategoryName)
            .ToList();

        return View("Index", categoryNames);
    }
}
