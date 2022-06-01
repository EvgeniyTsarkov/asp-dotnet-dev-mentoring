using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Controllers;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Services.Interfaces;
using NorthwindWebsite.Tests.TestDataProviders;

namespace NorthwindWebsite.Tests.ControllersTests;

public class CategoriesControllerTests
{
    private const string NormalImageFormat = "image/jpg";
    private const int NormalFileSize = 2_000_000;

    private readonly Mock<ICategoryService> _categoryServiceMock = new();
    private readonly CategoriesTestDataProvider _dataProvider = new();

    [Fact]
    public async Task IndexAction_ShouldReturnIndexViewAndModel()
    {
        //Arrange
        _categoryServiceMock.Setup(repo => repo.GetAll()).ReturnsAsync(_dataProvider.GetCategoriesAsync());

        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        var expectedCategories = _dataProvider.GetCategoriesAsync();

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
            .ReturnsAsync(CategoriesTestDataProvider.GetFileUploadModel(testCategoryId, NormalFileSize, "image/pdf"));

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

    public async Task ImageUpdateAction_ShouldReturnIndexViewIfModelIsCorrect_HttpPut()
    {
        //Arrange
        var categoryId = 1;

        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        var fileUploadModel = CategoriesTestDataProvider.GetFileUploadModel(categoryId, NormalFileSize, NormalImageFormat);

        //Act
        var result = await categoryController.ImageUpload(fileUploadModel);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("Index", viewResult.ViewName);
    }

    [Fact]
    public async Task ImageUpdateAction_ValidationShouldReturnUploadView_HttpPut()
    {
        //Arrange
        var categoryId = 1;

        var categoryController = new CategoriesController(_categoryServiceMock.Object);

        categoryController.ModelState.AddModelError(string.Empty, "Please select a file.");

        var fileUploadModel = CategoriesTestDataProvider.GetFileUploadModel(categoryId, NormalFileSize, NormalImageFormat);

        //Act
        var result = await categoryController.ImageUpload(fileUploadModel);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("Upload", viewResult.ViewName);
    }

    [Fact]
    public void BackToCategories_ShouldRedirectToIndexAction()
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
