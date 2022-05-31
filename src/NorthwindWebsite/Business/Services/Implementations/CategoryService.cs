using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly AppSettings _appSettings;

    public CategoryService(
        ICategoryRepository categoryRepository,
        AppSettings appSettings)
    {
        _categoryRepository = categoryRepository;
        _appSettings = appSettings;
    }

    public async Task<IEnumerable<Category>> GetAll() =>
        await _categoryRepository.GetAll();

    public async Task<Dictionary<int, string>> GetCategoryOptions()
    {
        var categories = await _categoryRepository.GetAll();

        return categories.ToDictionary(c => c.CategoryId, c => c.CategoryName);
    }

    public async Task<FileUploadDto> GetFileUploadModel(int id)
    {
        var fileUploadModel = new FileUploadDto
        {
            CategoryId = id,
            MaximumFileSize = _appSettings.FileUploadOptions.ImageMaxSize
        };

        return await Task.FromResult(fileUploadModel);
    }

    public async Task UpdateCategoryWithPicture(FileUploadDto fileUploadModel)
    {
        var category = await _categoryRepository.Get(fileUploadModel.CategoryId);

        using var memoryStream = new MemoryStream();

        await fileUploadModel.FileUpload.CopyToAsync(memoryStream);

        category.Picture = memoryStream.ToArray();

        await _categoryRepository.Update(category);
    }

    public async Task<byte[]> GetImage(int id) =>
        await _categoryRepository.GetImage(id);
}
