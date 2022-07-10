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

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _productService.Create(product);

        return CreatedAtAction(nameof(Create), product);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> Update(Product productUpdate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var productToUpdate = _productService.GetProduct(productUpdate.ProductId);

        if (productToUpdate == null)
        {
            return NotFound();
        }

        await _productService.Update(productUpdate);

        return productUpdate;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteById(int id)
    {
        var productToDelete = await _productService.GetProduct(id);

        if (productToDelete == null)
        {
            return NotFound();
        }

        await _productService.Delete(id);

        return NoContent();
    }
}
