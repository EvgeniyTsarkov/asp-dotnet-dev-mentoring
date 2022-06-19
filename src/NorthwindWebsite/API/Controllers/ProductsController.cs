using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Services.Interfaces;

namespace NorthwindWebsite.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet(Name = nameof(GetProducts))]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAll();

        return Ok(products.ToList());
    }
}
