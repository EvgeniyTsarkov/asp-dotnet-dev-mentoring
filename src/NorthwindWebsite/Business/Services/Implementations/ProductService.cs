using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure;

namespace NorthwindWebsite.Business.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;

        public ProductService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll() =>
            await _context.Products.AsQueryable<Product>()
                .Include(p => p.Supplier)
                .Include(p => p.Category)
            .ToListAsync();
    }
}
