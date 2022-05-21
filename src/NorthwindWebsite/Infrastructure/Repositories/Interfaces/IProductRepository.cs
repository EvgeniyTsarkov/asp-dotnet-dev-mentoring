using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
    }
}
