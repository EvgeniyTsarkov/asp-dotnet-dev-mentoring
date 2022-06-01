using NorthwindWebsite.Infrastructure.Entities;
using System.Linq.Expressions;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<byte[]> GetImage(int id);

    Task<Category> Update(Category category);
}
