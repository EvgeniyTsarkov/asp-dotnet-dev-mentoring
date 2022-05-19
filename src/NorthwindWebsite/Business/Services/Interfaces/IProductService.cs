using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
    }
}
