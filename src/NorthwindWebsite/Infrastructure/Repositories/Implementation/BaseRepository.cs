using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly NorthwindContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(NorthwindContext northwindContext)
        {
            _context = northwindContext;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll() =>
            await _dbSet.ToListAsync();
    }
}
