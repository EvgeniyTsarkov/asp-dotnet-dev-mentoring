using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Controllers;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Services.Interfaces;
using NorthwindWebsite.Tests.Factories;

namespace NorthwindWebsite.Tests.ControllersTests;

public class CategoriesControllerTests
{
    private const string NormalImageFormat = "image/jpg";
    private const int NormalFileSize = 2_000_000;

    private readonly Mock<ICategoryService> _categoryServiceMock = new();
    private readonly CategoriesTestDataProvider _dataProvider = new();

    [Fact]
    public async Task IndexAction_ShouldReturnCorrectViewAndModel()
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

        _categoryServiceMock.Verify(repo => repo.GetAll(),
            Times.AtLeastOnce(),
            "GetAll was never invoked.");
    }

    [Fact]
    public async Task ImageUpdateAction_ModelValidationShouldWorkCorrectly_HttpGet()
    {
        //Arrange
        var testCategoryId = 1;

        _categoryServiceMock.Setup(repo => repo.GetFileUploadModel(testCategoryId))
            .Returns(Task.FromResult(_dataProvider.GetFileUploadModel(testCategoryId, NormalFileSize, "image/pdf")));

        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        //Act
        var result = await categoryController.ImageUpload(testCategoryId);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<FileUploadDto>(viewResult.Model);

        Assert.NotNull(viewResult);
        Assert.Equal("Upload", viewResult.ViewName);
        Assert.NotNull(model);

        _categoryServiceMock.Verify(repo => repo.GetFileUploadModel(testCategoryId),
            Times.AtLeastOnce(),
            "GetAll was never invoked.");
    }

    public async Task ImageUpdateAction_ShouldReturnCorrectViewIfModelIsCorrect_HttpPut()
    {
        //Arrange
        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        var fileUploadModel = _dataProvider.GetFileUploadModel(1, NormalFileSize, NormalImageFormat);

        //Act
        var result = await categoryController.ImageUpload(fileUploadModel);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("Index", viewResult.ViewName);
    }

    [Fact]
    public async Task ImageUpdateAction_ShouldReturnCorrectViewAndModel_HttpPut()
    {
        //Arrange
        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        categoryController.ModelState.AddModelError(string.Empty, "Please select a file.");

        var fileUploadModel = _dataProvider.GetFileUploadModel(1, NormalFileSize, NormalImageFormat);

        //Act
        var result = await categoryController.ImageUpload(fileUploadModel);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("Upload", viewResult.ViewName);
    }

    [Fact]
    public void BackToCategories_ShouldRedirectToCorrectAction()
    {
        //Arrange
        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        //Act
        var result = categoryController.BackToCategories();

        //Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.NotNull(redirectToActionResult);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }
}
