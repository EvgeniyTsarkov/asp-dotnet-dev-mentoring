using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
