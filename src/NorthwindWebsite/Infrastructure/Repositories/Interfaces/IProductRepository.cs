using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAll();

    Task<List<Product>> GetLimitedNumberOfProducts(int limit);

    Task<Product> Get(int id, bool skipRelatedItems = false);

    Task<Product> Add(Product product);

    Task<Product> Update(Product product);

    Task Delete(int id);
}
