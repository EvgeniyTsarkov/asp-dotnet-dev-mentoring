using System.Linq.Expressions;

namespace NorthwindWebsite.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> Get(Expression<Func<TEntity, bool>> expression);
    }
}
