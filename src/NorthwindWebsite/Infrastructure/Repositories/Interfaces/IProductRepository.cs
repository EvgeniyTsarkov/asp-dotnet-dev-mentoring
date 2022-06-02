using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> GetLimitedNumberOfProducts(int limit);

    Task<Product> Get(int id, bool skipRelatedItems);

    Task<Product> Add(Product product);

    Task<Product> Update(Product product);

    Task Delete(int id);
}
