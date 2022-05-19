using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Business.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
    }
}
