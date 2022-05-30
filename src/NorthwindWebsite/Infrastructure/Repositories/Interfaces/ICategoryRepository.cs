using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> Get(int id);

    Task<byte[]> GetImage(int id);

    Task<Category> Update(Category category);
}
