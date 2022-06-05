using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Infrastructure.Repositories.Interfaces;
using NorthwindWebsite.Services.Interfaces;

namespace NorthwindWebsite.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IImageCachingService _imageCachingService;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IImageCachingService imageCachingService,
        ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _imageCachingService = imageCachingService;
        _logger = logger;
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
            CategoryId = id
        };

        return await Task.FromResult(fileUploadModel);
    }

    public async Task UpdateCategoryWithPicture(FileUploadDto fileUploadModel)
    {
        var category = await _categoryRepository.Get(c => c.CategoryId == fileUploadModel.CategoryId);

        using var memoryStream = new MemoryStream();

        await fileUploadModel.FileUpload.CopyToAsync(memoryStream);

        category.Picture = memoryStream.ToArray();

        await _categoryRepository.Update(category);
    }

    public async Task<byte[]> GetImage(int id)
    {
        var isContained = _imageCachingService.IsContained(id.ToString());

        if (isContained)
        {
            _logger.LogWarning("Getting image from cache.");
            return _imageCachingService.GetImageFromCache(id);
        }

        _logger.LogWarning("Getting image from database.");
        return await _categoryRepository.GetImage(id);
    }
}