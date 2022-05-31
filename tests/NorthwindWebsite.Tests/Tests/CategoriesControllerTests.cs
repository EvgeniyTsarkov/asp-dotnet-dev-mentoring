using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Controllers;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Infrastructure.Entities;
using NorthwindWebsite.Services.Interfaces;
using NorthwindWebsite.Tests.Factories;

namespace NorthwindWebsite.Tests.ControllersTests;

public class CategoriesControllerTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock = new();
    private readonly CategoriesTestDataProvider _dataProvider = new();
    private readonly AppSettings _appSettings = new AppSettings()
    {
        FileUploadOptions = new FileUploadOptions()
        {
            ImageFileFormats = "jpg, jpeg, bmp, png",
            ImageMaxSize = 200
        }
    };

    [Fact]
    public async Task IndexAction_ShouldReturnCorrectViewAndModel()
    {
        //Arrange
        _categoryServiceMock.Setup(repo => repo.GetAll()).Returns(_dataProvider.GetCategoriesAsync());

        var categoryController = new CategoriesController(
            _categoryServiceMock.Object, _appSettings);

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
            .Returns(_dataProvider.GetFileUploadModel(testCategoryId, 2_000_000, "image/pdf"));

        var categoryController = new CategoriesController(
            _categoryServiceMock.Object, _appSettings);

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
        var categoryController = new CategoriesController(
            _categoryServiceMock.Object, _appSettings);
        var fileUploadModel = await _dataProvider.GetFileUploadModel(1, 2_000_000, "image/jpg");

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
        var categoryController = new CategoriesController(
            _categoryServiceMock.Object, _appSettings);
        categoryController.ModelState.AddModelError(string.Empty, "Please select a file.");
        var fileUploadModel = await _dataProvider.GetFileUploadModel(1, 2_000_000, "image/jpeg");

        //Act
        var result = await categoryController.ImageUpload(fileUploadModel);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("Upload", viewResult.ViewName);
    }

    [Fact]
    public async Task ImageUpdateAction_ShouldReturnCorrectViewWhenFileExtensionIsIncorrect_HttpPut()
    {
        //Arrange
        var categoryController = new CategoriesController(
            _categoryServiceMock.Object, _appSettings);
        var fileUploadModel = await _dataProvider.GetFileUploadModel(1, 2_000_000, "application/pdf");

        //Act
        var result = await categoryController.ImageUpload(fileUploadModel);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("Upload", viewResult.ViewName);
    }

    [Fact]
    public async Task ImageUpdateAction_ShouldReturnCorrectViewWhenFileSizeIsIncorrect_HttpPut()
    {
        //Arrange
        var categoryController = new CategoriesController(
            _categoryServiceMock.Object, _appSettings);
        var fileUploadModel = await _dataProvider.GetFileUploadModel(1, 2_000_000_000, "image/jpg");

        //Act
        var result = await categoryController.ImageUpload(fileUploadModel);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("Upload", viewResult.ViewName);
    }
}
