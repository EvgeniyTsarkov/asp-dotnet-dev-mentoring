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

    public IActionResult Index()
    {
        var categoryNames = _categoryService.GetAll()
            .Select(category => category.CategoryName)
            .ToList();

        return View("~/Presentation/Views/Categories/Index.cshtml", categoryNames);
    }
}
