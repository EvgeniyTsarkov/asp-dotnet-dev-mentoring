using Microsoft.AspNetCore.Http;
using Moq;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Tests.Factories
{
    public class CategoriesTestDataProvider
    {
        private readonly IEnumerable<Category> _categories = new List<Category>()
            {
                new()
                {
                    CategoryId = 1,
                    CategoryName = "Beverages",
                    Description = "Beverages",
                    Picture = null
                },
                new()
                {
                    CategoryId = 2,
                    CategoryName = "Condiments",
                    Description = "Condiments",
                    Picture = null
                },
                new()
                {
                    CategoryId = 3,
                    CategoryName = "Confections",
                    Description = "Confections",
                    Picture = null
                },
                new()
                {
                    CategoryId = 4,
                    CategoryName = "Dairy Products",
                    Description = "Dairy Products",
                    Picture = null
                }
            };

        public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
            await Task.FromResult(_categories);

        public async Task<FileUploadDto> GetFileUploadModel(int id, int fileLength, string contentType)
        {
            var fileUploadModel = new FileUploadDto
            {
                CategoryId = id,
                MaximumFileSize = 200_000,
                FileUpload = BuildFormFile(fileLength, contentType)
            };

            return await Task.FromResult(fileUploadModel);
        }

        private static IFormFile BuildFormFile(int fileLength, string contentType)
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(ff => ff.FileName).Returns("file name");
            fileMock.Setup(ff => ff.Length).Returns(fileLength);
            fileMock.Setup(ff => ff.ContentType).Returns(contentType);

            return fileMock.Object;
        }
    }
}
