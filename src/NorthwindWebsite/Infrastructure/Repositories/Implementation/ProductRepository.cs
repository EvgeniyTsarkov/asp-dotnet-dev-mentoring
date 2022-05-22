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

        public async Task<Product> Get(int id) =>
            await _context.Products
                .AsNoTracking()
                .AsQueryable<Product>()
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

        public async Task<Product> Add(Product productToAdd)
        {
            await _context.Products.AddAsync(productToAdd);
            await _context.SaveChangesAsync();
            return productToAdd;
        }

        public async Task Delete(int id)
        {
            var productToDelete = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (productToDelete == null) return;

            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
