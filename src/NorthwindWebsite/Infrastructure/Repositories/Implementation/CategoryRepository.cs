using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Core.CustomExceptions.InfrastructureExceptions;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;

namespace NorthwindWebsite.Infrastructure.Repositories.Implementation;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(NorthwindContext northwindContext)
        : base(northwindContext)
    {
    }

    public async Task<Category> Get(int id) =>
        await _context.Categories
            .AsQueryable()
            .FirstOrDefaultAsync(c => c.CategoryId == id);

    public async Task<byte[]> GetImage(int id)
    {
        var category = await Get(id);

        if (category == null)
        {
            throw new RecordNotFoundException(string.Format("Category with id {0} not found", id));
        }

        return category.Picture;
    }

    public async Task<Category> Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return category;
    }
}
