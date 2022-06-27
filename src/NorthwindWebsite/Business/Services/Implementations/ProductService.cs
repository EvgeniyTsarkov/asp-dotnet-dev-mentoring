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

    public async Task<ProductsDto> GetAll() =>
        new ProductsDto
        {
            Products = await _productRepository.GetAll()
        };


    public async Task<ProductsDto> GetProducts()
    {
        var productsListDto = new ProductsDto();

        var maximumProductsOnPage = _appSettings.MaximumProductsOnPage;

        productsListDto.Products = maximumProductsOnPage <= 0 ?
            await _productRepository.GetAll() :
            await _productRepository.GetLimitedNumberOfProducts(maximumProductsOnPage);

        return productsListDto;
    }

    public async Task<ProductHandleDto> GetProductModel(int id)
    {
        var productToCreateOrUpdate = new ProductHandleDto
        {
            Product = await _productRepository.Get(id, false) ?? new Product(),

            CategoryOptions = await _categoryService.GetCategoryOptions(),

            SupplierOptions = await _supplierService.GetSupplierOptions()
        };

        return productToCreateOrUpdate;
    }

    public async Task<Product> Update(Product product) =>
        await _productRepository.Update(product);

    public async Task<Product> Create(Product product) =>
        await _productRepository.Add(product);

    public async Task Delete(int id) =>
        await _productRepository.Delete(id);
}
