using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Presentation.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    private readonly ICategoryService _categoryService;

    private readonly ISupplierService _supplierService;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        ISupplierService supplierService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _supplierService = supplierService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.BuildProductListDto();

        return View("Index", products);
    }

    [HttpGet]
    public async Task<IActionResult> CreateOrUpdate(int id)
    {
        var productToCreateOrUpdate = await _productService.BuildProductCreateOrUpdate(id);

        ViewData["CategoryOptions"] = await _categoryService.GetSelectListItems();

        ViewData["SupplierOptions"] = await _supplierService.GetSelectListItems();

        return View("CreateOrUpdate", productToCreateOrUpdate);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOrUpdate(Product productToCreateOrUpdate)
    {
        ViewData["CategoryOptions"] = await _categoryService.GetSelectListItems();

        ViewData["SupplierOptions"] = await _supplierService.GetSelectListItems();


        //Console.WriteLine(productToCreateOrUpdate);

        if (!ModelState.IsValid)
        {
            return View("CreateOrUpdate", productToCreateOrUpdate);
        }

        if (productToCreateOrUpdate.ProductId == 0)
        {
            await _productService.Create(productToCreateOrUpdate);
            return RedirectToAction("Index");
        }

        await _productService.Update(productToCreateOrUpdate);

        return RedirectToAction("Index");
    }

    [ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.Delete(id);

        return RedirectToAction("Index");
    }

    [ActionName("BackToProducts")]
    public IActionResult BackToMain() => RedirectToAction("Index");
}
