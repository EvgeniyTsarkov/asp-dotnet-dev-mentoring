using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();

    Task<Category> Get(int id);

    Task Update(Category category);

    Task<Dictionary<int, string>> GetCategoryOptions();

    Task<FileUploadDto> GetFileUploadModel(int id);

    Task UpdateCategoryWithPicture(FileUploadDto fileUploadModel);

    Task<byte[]> GetImage(int id);
}
