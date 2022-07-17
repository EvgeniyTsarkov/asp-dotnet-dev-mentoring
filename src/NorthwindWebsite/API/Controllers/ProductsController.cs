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

    /// <summary>
    /// Returns all products from the database.
    /// </summary>
    /// <returns><see cref="Task">Represents an asynchronous operation.</returns>
    [HttpGet(Name = nameof(GetProducts))]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _productService.GetSimpleProducts();

        return products.Products;
    }

    /// <summary>
    /// Returns a product from the database by id.
    /// </summary>
    /// <param name="id">Product id.</param>
    /// <returns><see cref="Task">Represents an asynchronous operation.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    /// <summary>
    /// Creates a product in the database.
    /// </summary>
    /// <param name="product"><see cref="Product"></param>
    /// <response code="201">Product successfully created in the database.</response>
    /// <response code="400">The product does not have all the required values or these values are incorrect.</response>
    /// <returns><see cref="Task">Represents an asynchronous operation.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _productService.Create(product);

        return CreatedAtAction(nameof(Create), product);
    }

    /// <summary>
    /// Updates a product in the database.
    /// </summary>
    /// <param name="productUpdate"><see cref="Product"></param>
    /// <response code="400">The product does not have all the required values or these values are incorrect.</response>
    /// <response code="404">The product to be updated is not found in the database.</response>
    /// <returns><see cref="Task">Represents an asynchronous operation.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Deletes a product in the database.
    /// </summary>
    /// <param name="id">Id of the product to be deleted.</param>
    /// <response code="204">The product is successfully deleted from the database.</response>
    /// <response code="404">The product to be deleted is not found in the database.</response>
    /// <returns><see cref="Task">Represents an asynchronous operation.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteById(int id)
    {
        await _productService.Delete(id);

        return NoContent();
    }
}
