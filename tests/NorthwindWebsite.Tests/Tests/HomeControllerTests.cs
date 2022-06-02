using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindWebsite.Presentation.Controllers;

namespace NorthwindWebsite.Tests.Tests;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _loggerMock = new();

    [Fact]
    public void IndexAction_ShouldReturnIndexView()
    {
        //Arrange
        var homeController = new HomeController(_loggerMock.Object);

        //Act
        var result = homeController.Index();

        //Assert
        var actionResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(result);
        Assert.Equal("Index", actionResult.ViewName);
    }

    [Fact]
    public void PrivacyAction_ShouldReturnPrivacyView()
    {
        //Arrange
        var homeController = new HomeController(_loggerMock.Object);

        //Act
        var result = homeController.Privacy();

        //Assert
        var actionResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(result);
        Assert.Equal("Privacy", actionResult.ViewName);
    }
}
