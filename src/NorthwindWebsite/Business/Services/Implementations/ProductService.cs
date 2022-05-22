using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    private readonly AppSettings _appSettings;

    public ProductService(
        IProductRepository productRepository,
        AppSettings appSettings)
    {
        _productRepository = productRepository;
        _appSettings = appSettings;
    }

    public async Task<ProductsListDto> BuildProductListDto()
    {
        var productsListDto = new ProductsListDto();

        var maximumProductsOnPage = _appSettings.MaximumProductsOnPage;

        productsListDto.Products = maximumProductsOnPage <= 0 ?
            await _productRepository.GetAll() :
            await _productRepository.GetLimitedNumberOfProducts(maximumProductsOnPage);

        return productsListDto;
    }
}
