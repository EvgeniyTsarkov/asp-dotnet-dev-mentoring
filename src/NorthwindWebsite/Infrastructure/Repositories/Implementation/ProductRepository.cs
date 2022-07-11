using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Core.CustomExceptions.InfrastructureExceptions;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(NorthwindContext context)
        : base(context)
    {
    }

    public new async Task<List<Product>> GetAll() =>
        await _context.Products
        .AsNoTracking()
        .AsQueryable()
        .Include(p => p.Supplier)
        .Include(p => p.Category)
        .ToListAsync();

    public async Task<List<Product>> GetLimitedNumberOfProducts(int limit) =>
        await _context.Products
        .AsNoTracking()
        .AsQueryable()
        .Include(p => p.Supplier)
        .Include(p => p.Category)
        .Take(limit)
        .ToListAsync();

    public async Task<List<Product>> GetSimpleProductsRepresentation() =>
        await _context.Products.ToListAsync();

    public async Task<Product> Get(int id, bool skipRelatedItems)
    {
        if (skipRelatedItems)
        {
            return await Get(p => p.ProductId == id);
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
            throw new RecordNotFoundException(string.Format("Product with id {0} not found in database", id));
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
