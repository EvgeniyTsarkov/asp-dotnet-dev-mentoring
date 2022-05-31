using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly AppSettings _appSettings;

    public CategoriesController(
        ICategoryService categoryService,
        AppSettings appSettings)
    {
        _categoryService = categoryService;
        _appSettings = appSettings;
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
        fileUploadModel.MaximumFileSize = _appSettings.FileUploadOptions.CategoryPicturesMaxSize;

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Please select a file.");
            return View("Upload", fileUploadModel);
        }

        var permittedExtensions = _appSettings.FileUploadOptions.CategoryPictureFileFormats
            .Split(", ").ToArray();

        var uploadedFileExtension = fileUploadModel.FileUpload.ContentType.Split("/")[1];

        if (!permittedExtensions.Contains(uploadedFileExtension))
        {
            ModelState.AddModelError(string.Empty,
                String.Format("Wrong file format({0}). Please use .jpg, .jpeg, .bmp, .png", uploadedFileExtension));
            return View("Upload", fileUploadModel);
        }

        if (fileUploadModel.FileUpload.Length > fileUploadModel.MaximumFileSize)
        {
            ModelState.AddModelError(string.Empty,
                string.Format("The file is too large({0} kB). Maximum file size is {1} kB.",
                fileUploadModel.FileUpload.Length / 1000, fileUploadModel.MaximumFileSize / 1000));
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

        return File(imageByteArray, "image/bmp");
    }

    public IActionResult BackToCategories() =>
        RedirectToAction("Index");
}
