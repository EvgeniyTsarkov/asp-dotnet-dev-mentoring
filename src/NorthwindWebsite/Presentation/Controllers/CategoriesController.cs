using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Core.Constants;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(
        ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categoriesList = await _categoryService.GetAll();

        var categories = categoriesList.ToList();

        return View("Index", categories);
    }

    [HttpGet]
    public async Task<IActionResult> ImageUpload(int id)
    {
        var fileUploadModel = await _categoryService.GetFileUploadModel(id);

        return View("Upload", fileUploadModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImageUpload(FileUploadDto fileUploadModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Please select a file.");

            return View("Upload", fileUploadModel);
        }

        await _categoryService.UpdateCategoryWithPicture(fileUploadModel);

        return RedirectToAction("Index", "Categories");
    }

    public async Task<IActionResult> Images(int id)
    {
        var imageByteArray = await _categoryService.GetImage(id);

        if (imageByteArray == null)
        {
            return NotFound();
        }

        return File(imageByteArray, HttpContentConstants.ImageBmp);
    }

    public IActionResult FromApi() =>
        View("FromApi");

    public IActionResult BackToCategories() =>
        RedirectToAction("Index");
}
