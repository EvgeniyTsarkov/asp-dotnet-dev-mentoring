using Moq;
using NorthwindWebsite.Business.CustomValidators;
using NorthwindWebsite.Core.ApplicationSettings;
using NorthwindWebsite.Tests.Factories;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Tests.Tests
{
    public class CustomAttributesTests
    {
        private const string NormalImageFormat = "image/jpg";
        private const int NormalFileSize = 2_000_000;

        private readonly Mock<IServiceProvider> _serviceProviderMock = new();
        private readonly CategoriesTestDataProvider _dataProvider = new();
        private readonly AppSettings _appSettings = new AppSettings
        {
            FileUploadOptions = new FileUploadOptions
            {
                ImageMaxSize = 200000,
                ImageFileFormats = new[] { "jpg", "jpeg", "bmp", "png" }
            }
        };

        [Fact]
        public async Task AllowedImageFileTypesAttributeShouldReturnErrorMessageWhenValidationFails()
        {
            //Arrange
            var fileUploadModel = _dataProvider.GetFileUploadModel(1, NormalFileSize, "application/pdf");

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
        public async Task AllowedImageFileTypesAttributeShouldReturnCorrectResultWhenValidationSuceeds()
        {
            //Arrange
            var fileUploadModel = _dataProvider.GetFileUploadModel(1, NormalFileSize, "image/jpg");

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
        public async Task ImageFileSizeLimitAttributeShouldReturnErrorMessageWhenValidationFails()
        {
            //Arrange
            var fileUploadModel = _dataProvider.GetFileUploadModel(1, 2_000_000_000, NormalImageFormat);

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
        public async Task ImageFileSizeLimitAttributeShouldReturnCorrectResultWhenValidationSucceeds()
        {
            //Arrange
            var fileUploadModel = _dataProvider.GetFileUploadModel(1, 20, NormalImageFormat);

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
}
