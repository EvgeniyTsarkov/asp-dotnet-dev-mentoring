using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;

namespace NorthwindWebsite.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        private readonly IConfiguration _configuration;

        public ProductsController(
            IProductService productService,
            IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var products = new ProductsListDto();

            var listOfProducts = await _productService.GetAll();
            int.TryParse(_configuration["MaximumProductsOnPage"], out int maximumProducts);

            products.Products = listOfProducts.ToList();
            products.MaximumProductsOnPage =
                (maximumProducts <= 0 ? products.Products.Count : maximumProducts);

            return View("Index", products);
        }
    }
}
