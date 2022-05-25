using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext context)
            : base(context)
        {
        }

        public new async Task<List<Product>> GetAll() =>
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

        public async Task<Product> Get(int id, bool skipRelatedItems = false)
        {
            if (skipRelatedItems)
            {
                return await _context.Products
                    .AsNoTracking()
                    .SingleOrDefaultAsync(p => p.ProductId == id);
            }

            return await _context.Products
                .AsNoTracking()
                .AsQueryable()
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products
                .SingleOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
