using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Core.Application_Settings;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Business.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IAppSettings _appSettings;

        private readonly IConfiguration _configuration;

        public ProductService(
            IProductRepository productRepository,
            IAppSettings appSettings,
            IConfiguration configuration)
        {
            _productRepository = productRepository;
            _appSettings = appSettings;
            _configuration = configuration;
        }

        public async Task<ProductsListDto> BuildProductListDto()
        {
            var appSettings = _appSettings.ReadAppSettings(_configuration);
            int.TryParse(appSettings.MaximumProductsOnPage, out var maximumProducts);

            var productsListDto = new ProductsListDto();

            productsListDto.Products = maximumProducts <= 0 ?
                await _productRepository.GetAll() :
                await _productRepository.GetLimitedNumberOfProducts(maximumProducts);

            return productsListDto;
        }
    }
}
