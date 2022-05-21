using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;

namespace NorthwindWebsite.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ProductsListDto products = new ProductsListDto(); 
            
            var listOfProducts = await _productService.GetAll();

            products.Products = listOfProducts.ToList();

            return View("Index", products);
        }
    }
}
