using NorthwindWebsite.Entities;

namespace NorthwindWebsite.Services.Interfaces;

public interface ICategoryService
{
    IEnumerable<Category> GetAll();
}
