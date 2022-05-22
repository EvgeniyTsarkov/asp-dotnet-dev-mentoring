using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<List<Product>> GetLimitedNumberOfProducts(int limit);
    }
}
