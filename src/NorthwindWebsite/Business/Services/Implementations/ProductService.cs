using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    private readonly ICategoryRepository _categoryRepository;

    private readonly ISupplierRepository _supplierRepository;

    private readonly AppSettings _appSettings;

    public ProductService(
        IProductRepository productRepository,
        AppSettings appSettings,
        ICategoryRepository categoryRepository,
        ISupplierRepository supplierRepository)
    {
        _productRepository = productRepository;
        _appSettings = appSettings;
        _categoryRepository = categoryRepository;
        _supplierRepository = supplierRepository;
    }

    public async Task<ProductsDto> BuildProductListDto()
    {
        var productsListDto = new ProductsDto();

        var maximumProductsOnPage = _appSettings.MaximumProductsOnPage;

        productsListDto.Products = maximumProductsOnPage <= 0 ?
            await _productRepository.GetAll() :
            await _productRepository.GetLimitedNumberOfProducts(maximumProductsOnPage);

        return productsListDto;
    }

    public async Task<Product> BuildProductCreateOrUpdate(int id)
    {
        var productToCreateOrUpdate =
            await _productRepository.Get(id) ?? new Product();

        return productToCreateOrUpdate;
    }
}
