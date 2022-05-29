using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindWebsite.Presentation.Controllers;

namespace NorthwindWebsite.Tests.Tests;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _loggerMock = new();

    [Fact]
    public void Index_VerifyCorrectViewIsReturned()
    {
        //Arrange
        var homeController = new HomeController(_loggerMock.Object);

        //Act
        var result = homeController.Index();

        //Assert
        var actionResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(result);
        Assert.Equal(expected: "Index", actual: actionResult.ViewName);
    }

    [Fact]
    public void Privacy_VerifyCorrectViewIsReturned()
    {
        //Arrange
        var homeController = new HomeController(_loggerMock.Object);

        //Act
        var result = homeController.Privacy();

        //Assert
        var actionResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(result);
        Assert.Equal(expected: "Privacy", actual: actionResult.ViewName);
    }
}
