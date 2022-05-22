using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation
{
    public class SupplierRepository : BaseRepository, ISupplierRepository
    {
        public SupplierRepository(NorthwindContext northwindContext) : base(northwindContext)
        {
        }

        public async Task<IEnumerable<Supplier>> GetAll() =>
            await _context.Suppliers.ToListAsync();
    }
}
