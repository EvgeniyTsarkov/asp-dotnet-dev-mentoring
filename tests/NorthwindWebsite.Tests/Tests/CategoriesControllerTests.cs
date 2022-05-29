using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindWebsite.Controllers;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Services.Interfaces;
using NorthwindWebsite.Tests.Factories;

namespace NorthwindWebsite.Tests.ControllersTests;

public class CategoriesControllerTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock = new();

    private readonly CategoriesTestDataProvider _dataProvider = new();

    [Fact]
    public async Task IndexAction_VerifyCorrectViewAndModelAreReturned()
    {
        //Arrange
        _categoryServiceMock.Setup(repo => repo.GetAll()).Returns(_dataProvider.GetCategoriesAsync());

        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        var expectedCategories = await _dataProvider.GetCategoriesAsync();

        //Act
        var result = await categoryController.Index();

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IList<Category>>(viewResult.Model);

        Assert.NotNull(viewResult);
        Assert.Equal("Index", viewResult.ViewName);
        Assert.NotNull(model);
        Assert.Equal(expectedCategories.Count(), model.Count);
    }
}
