using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            var productsList = _productService.GetAll().ToList();

            return View("~/Presentation/Views/Products/Index.cshtml", productsList);
        }
    }
}
