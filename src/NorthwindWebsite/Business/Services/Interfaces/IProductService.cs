using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Services.Interfaces;

public interface IProductService
{
    Task<ProductsDto> BuildProductListDto();

    Task<ProductToCreateOrUpdateDto> BuildProductCreateOrUpdate(int id);

    Task<Product> Create(Product productToCreate);

    Task<Product> Update(Product productToUpdate);

    Task Delete(int id);
}
