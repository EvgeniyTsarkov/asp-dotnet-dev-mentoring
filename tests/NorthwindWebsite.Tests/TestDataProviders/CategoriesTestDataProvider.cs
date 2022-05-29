using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Tests.Factories
{
    public class CategoriesTestDataProvider
    {
        private readonly IEnumerable<Category> _categories = new List<Category>()
            {
                new() {CategoryId = 1, CategoryName = "Beverages",
                    Description = "Beverages", Picture = null},
                new() {CategoryId = 2, CategoryName = "Condiments",
                    Description = "Condiments", Picture = null},
                new() {CategoryId = 3, CategoryName = "Confections",
                    Description = "Confections", Picture = null},
                new() {CategoryId = 4, CategoryName ="Dairy Products" }
            };

        public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
            await Task.Run(() => _categories);
    }
}
