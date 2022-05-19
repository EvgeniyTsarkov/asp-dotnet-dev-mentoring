using Microsoft.EntityFrameworkCore;
using NorthwindWebsite.Infrastructure;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly NorthwindContext _context;

    public CategoryService(NorthwindContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAll() =>
        await _context.Categories.ToListAsync();
}
