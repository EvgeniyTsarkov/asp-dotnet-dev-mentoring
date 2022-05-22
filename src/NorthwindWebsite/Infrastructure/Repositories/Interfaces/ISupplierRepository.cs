using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAll();
    }
}
