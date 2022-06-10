using Moq;
using NorthwindWebsite.Business.CustomValidators;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Tests.TestDataProviders;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Tests.Tests;

public class CustomAttributesTests
{
    private const string NormalImageFormat = "image/jpg";
    private const int NormalFileSize = 2_000_000;

    private readonly Mock<IServiceProvider> _serviceProviderMock = new();
    private readonly AppSettings _appSettings = new()
    {
        FileUploadOptions = new FileUploadOptions
        {
            ImageMaxSize = 200000,
            ImageFileFormats = new[] { "jpg", "jpeg", "bmp", "png" }
        }
    };

    [Fact]
    public async Task AllowedImageFileTypesAttribute_ShouldReturnErrorMessageWhenValidationFails()
    {
        //Arrange
        var categoryId = 1;

        var fileUploadModel = CategoriesTestDataProvider.GetFileUploadModel(categoryId, NormalFileSize, "application/pdf");

        _serviceProviderMock.Setup(appSets => appSets.GetService(typeof(AppSettings))).Returns(_appSettings);

        var validationContext = new ValidationContext(fileUploadModel.FileUpload, _serviceProviderMock.Object, null);

        var allowedFileTypesAttribute = new AllowedImageFileTypesAttribute();

        //Act
        var validationResult = allowedFileTypesAttribute.GetValidationResult(fileUploadModel.FileUpload, validationContext);

        //Assert
        Assert.NotNull(validationResult);
        Assert.NotNull(validationResult!.ErrorMessage);
        Assert.NotEmpty(validationResult.ErrorMessage);

        _serviceProviderMock.Verify(repo => repo.GetService(typeof(AppSettings)),
            Times.AtLeastOnce(),
            "GetAll was never invoked.");
    }

    [Fact]
    public async Task AllowedImageFileTypesAttribute_ShouldReturnCorrectResultWhenValidationSuceeds()
    {
        //Arrange
        var categoryId = 1;

        var fileUploadModel = CategoriesTestDataProvider.GetFileUploadModel(categoryId, NormalFileSize, "image/jpg");

        _serviceProviderMock.Setup(appSets => appSets.GetService(typeof(AppSettings))).Returns(_appSettings);

        var validationContext = new ValidationContext(fileUploadModel.FileUpload, _serviceProviderMock.Object, null);

        var allowedFileTypesAttribute = new AllowedImageFileTypesAttribute();

        //Act
        var validationResult = allowedFileTypesAttribute.GetValidationResult(fileUploadModel.FileUpload, validationContext);

        //Assert
        Assert.Equal(ValidationResult.Success, validationResult);

        _serviceProviderMock.Verify(repo => repo.GetService(typeof(AppSettings)),
            Times.AtLeastOnce(),
            "GetAll was never invoked.");
    }

    [Fact]
    public async Task ImageFileSizeLimitAttribute_ShouldReturnErrorMessageWhenValidationFails()
    {
        //Arrange
        var categoryId = 1;

        var fileUploadModel = CategoriesTestDataProvider.GetFileUploadModel(categoryId, 2_000_000_000, NormalImageFormat);

        _serviceProviderMock.Setup(appSets => appSets.GetService(typeof(AppSettings))).Returns(_appSettings);

        var validationContext = new ValidationContext(fileUploadModel.FileUpload, _serviceProviderMock.Object, null);

        var maximumFileSizeAttribute = new ImageFileSizeLimitAttribute();

        //Act
        var validationResult = maximumFileSizeAttribute.GetValidationResult(fileUploadModel.FileUpload, validationContext);

        //Assert
        Assert.NotNull(validationResult);
        Assert.NotNull(validationResult!.ErrorMessage);
        Assert.NotEmpty(validationResult.ErrorMessage);

        _serviceProviderMock.Verify(repo => repo.GetService(typeof(AppSettings)),
            Times.AtLeastOnce(),
            "GetAll was never invoked.");
    }

    [Fact]
    public async Task ImageFileSizeLimitAttribute_ShouldReturnCorrectResultWhenValidationSucceeds()
    {
        //Arrange
        var categoryId = 1;

        var fileUploadModel = CategoriesTestDataProvider.GetFileUploadModel(categoryId, 20, NormalImageFormat);

        _serviceProviderMock.Setup(appSets => appSets.GetService(typeof(AppSettings))).Returns(_appSettings);

        var validationContext = new ValidationContext(fileUploadModel.FileUpload, _serviceProviderMock.Object, null);

        var maximumFileSizeAttribute = new ImageFileSizeLimitAttribute();

        //Act
        var validationResult = maximumFileSizeAttribute.GetValidationResult(fileUploadModel.FileUpload, validationContext);

        //Assert
        Assert.Equal(ValidationResult.Success, validationResult);

        _serviceProviderMock.Verify(repo => repo.GetService(typeof(AppSettings)),
            Times.AtLeastOnce(),
            "GetAll was never invoked.");
    }
}
