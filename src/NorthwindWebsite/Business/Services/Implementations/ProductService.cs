using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly AppSettings _appSettings;
    private readonly ICategoryService _categoryService;
    private readonly ISupplierService _supplierService;

    public ProductService(
        IProductRepository productRepository,
        AppSettings appSettings,
        ICategoryService categoryService,
        ISupplierService supplierService)
    {
        _productRepository = productRepository;
        _appSettings = appSettings;
        _categoryService = categoryService;
        _supplierService = supplierService;
    }

    public async Task<ProductsDto> GetProducts()
    {
        var productsListDto = new ProductsDto();

        var maximumProductsOnPage = _appSettings.MaximumProductsOnPage;

        productsListDto.Products = maximumProductsOnPage <= 0 ?
            await _productRepository.GetAll() :
            await _productRepository.GetLimitedNumberOfProducts(maximumProductsOnPage);

        return productsListDto;
    }

    public async Task<ProductToCreateOrUpdateDto> GetProductModel(int id)
    {
        var productToCreateOrUpdate = new ProductToCreateOrUpdateDto();

        var product = await _productRepository.Get(id) ?? new Product();

        productToCreateOrUpdate.Product = product;

        productToCreateOrUpdate.CategoryOptions = await _categoryService.GetCategorySelectList();

        productToCreateOrUpdate.SupplierOptions = await _supplierService.GetSupplerSelectList();

        return productToCreateOrUpdate;
    }

    public async Task<Product> Update(Product product) =>
        await _productRepository.Update(product);

    public async Task<Product> Create(Product product) =>
        await _productRepository.Add(product);

    public async Task Delete(int id) =>
        await _productRepository.Delete(id);
}
