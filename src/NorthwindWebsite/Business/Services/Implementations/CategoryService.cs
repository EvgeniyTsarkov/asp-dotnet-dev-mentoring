using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAll() =>
        await _categoryRepository.GetAll();

    public async Task<SelectList> GetCategorySelectList()
    {
        var categories = await GetAll();

        return new SelectList(categories, "CategoryId", "CategoryName");
    }
}
