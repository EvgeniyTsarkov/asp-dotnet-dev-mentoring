using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Models;
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
        CategoriesListDto categories = new CategoriesListDto();

        var categoriesList = await _categoryService.GetAll();

        categories.Categories = categoriesList.ToList();

        return View("Index", categories);
    }
}
