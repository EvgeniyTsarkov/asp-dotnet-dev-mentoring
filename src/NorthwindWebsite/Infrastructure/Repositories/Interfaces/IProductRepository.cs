using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAll();

    Task<List<Product>> GetLimitedNumberOfProducts(int limit);

    Task<Product> Get(int id);

    Task<Product> Add(Product productToAdd);

    Task<Product> Update(Product productToUpdate);

    Task Delete(int id);
}
