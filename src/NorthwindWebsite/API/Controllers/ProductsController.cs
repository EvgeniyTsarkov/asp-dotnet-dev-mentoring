using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Entities;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _productService.GetAll();

        return products.Products;
    }
}
