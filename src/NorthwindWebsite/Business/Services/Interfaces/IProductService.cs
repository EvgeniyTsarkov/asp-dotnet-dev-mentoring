using NorthwindWebsite.Business.Models;

namespace NorthwindWebsite.Business.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductsListDto> BuildProductListDto();
    }
}
