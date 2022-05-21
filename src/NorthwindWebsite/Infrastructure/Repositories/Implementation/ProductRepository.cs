using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(NorthwindContext context)
            : base(context)
        {
        }

        public async Task<List<Product>> GetAll() =>
            await _context.Products.AsQueryable<Product>()
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .ToListAsync();

        public async Task<List<Product>> GetLimitedNumberOfProducts(int limit) =>
            await _context.Products.AsQueryable<Product>()
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .Take(limit)
            .ToListAsync();
    }
}
