using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(NorthwindContext northwindContext) : base(northwindContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAll() =>
            await _context.Categories.ToListAsync();
    }
}
