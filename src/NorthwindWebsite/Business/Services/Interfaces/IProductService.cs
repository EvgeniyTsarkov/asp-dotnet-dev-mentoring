using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductsDto> BuildProductListDto();

        Task<Product> BuildProductCreateOrUpdate(int id);
    }
}
