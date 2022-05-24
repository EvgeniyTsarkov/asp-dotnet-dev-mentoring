using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation
{
    public class GenericRepository<TEntity> : BaseRepository, IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(NorthwindContext northwindContext) : base(northwindContext)
        {
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll() =>
            await _dbSet.ToListAsync();
    }
}
