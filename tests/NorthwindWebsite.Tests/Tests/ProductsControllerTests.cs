using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Business.Services.Interfaces;
using NorthwindWebsite.Entities;
using NorthwindWebsite.Presentation.Controllers;
using NorthwindWebsite.Services.Interfaces;
using NorthwindWebsite.Tests.TestDataFactories;

namespace NorthwindWebsite.Tests.Tests;

public class ProductsControllerTests
{
    private readonly Mock<IProductService> _productServiceMock = new();
    private readonly Mock<ICategoryService> _categoryServiceMock = new();
    private readonly Mock<ISupplierService> _supplierServiceMock = new();
    private readonly ProductsTestDataProvider _dataProvider = new();

    [Fact]
    public async Task IndexAction_ShouldReturnCorrectViewAndModel()
    {
        //Arrange
        _productServiceMock
            .Setup(repo => repo.GetProducts())
            .Returns(_dataProvider.GetProductsAsync())
            .Verifiable();

        var productsController = new ProductsController(
            _productServiceMock.Object,
            _categoryServiceMock.Object,
            _supplierServiceMock.Object);

        var expectedProductDto = await _dataProvider.GetProductsAsync();

        //Act
        var result = await productsController.Index();

        //Assert
        _productServiceMock.Verify(repo => repo.GetProducts(),
            Times.AtLeastOnce(),
            "GetProducts was never invoked.");

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<ProductsDto>(viewResult.Model);

        Assert.NotNull(viewResult);
        Assert.Equal("Index", viewResult.ViewName);
        Assert.NotNull(model);
        Assert.NotNull(model.Products);
        Assert.NotEmpty(model.Products);
        Assert.Equal(expectedProductDto.Products.Count, model.Products.Count);
    }

    [Fact]
    public async Task HandleAction_ShouldReturnCorrectViewAndModel()
    {
        //Arrange
        var testProductId = 1;

        _productServiceMock.Setup(repo => repo.GetProductModel(testProductId))
            .Returns(_dataProvider.GetProductModelAsync(testProductId));

        var productsController = new ProductsController(
            _productServiceMock.Object,
            _categoryServiceMock.Object,
            _supplierServiceMock.Object);

        var expectedProduct = new Product()
        {
            ProductId = testProductId,
            ProductName = "Chai",
        };

        //Act
        var result = await productsController.Handle(testProductId);

        //Assert
        _productServiceMock.Verify(repo => repo.GetProductModel(testProductId),
            Times.Once(),
            "GetProductModel was never invoked.");

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<ProductHandleDto>(viewResult.Model);

        Assert.NotNull(viewResult);
        Assert.Equal("CreateOrUpdate", viewResult.ViewName);
        Assert.NotNull(model);
        Assert.NotNull(model.Product);
        Assert.Equal(expectedProduct.ProductName, model.Product.ProductName);
    }

    [Fact]
    public async Task CreateAction_ModelValidationShouldWorksCorrectly()
    {
        //Arrange
        var productsController = new ProductsController(
            _productServiceMock.Object,
            _categoryServiceMock.Object,
            _supplierServiceMock.Object);

        productsController.ModelState.AddModelError("Product.UnitsInStock", "Required");

        //Act
        var result = await productsController.Create(new ProductHandleDto());

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("CreateOrUpdate", viewResult.ViewName);
    }

    [Fact]
    public async Task CreateAction_ShouldRedirectToCorrectAction()
    {
        //Arrange
        var productsController = new ProductsController(
            _productServiceMock.Object,
           _categoryServiceMock.Object,
           _supplierServiceMock.Object);

        //Act
        var result = await productsController.Create(new ProductHandleDto());

        //Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.NotNull(redirectToActionResult);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task UpdateAction_ModelValidationShouldWorkCorrectly()
    {
        //Arrange
        var productsController = new ProductsController(
            _productServiceMock.Object,
            _categoryServiceMock.Object,
            _supplierServiceMock.Object);

        productsController.ModelState.AddModelError("Product.UnitsInStock", "Required");

        //Act
        var result = await productsController.Update(new ProductHandleDto());

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.NotNull(viewResult);
        Assert.Equal("CreateOrUpdate", viewResult.ViewName);
    }

    [Fact]
    public async Task UpdateAction_ShouldRedirectToCorrectAction()
    {
        //Arrange
        var productsController = new ProductsController(
            _productServiceMock.Object,
           _categoryServiceMock.Object,
           _supplierServiceMock.Object);

        //Act
        var result = await productsController.Update(new ProductHandleDto());

        //Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.NotNull(redirectToActionResult);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task DeleteAction_ShouldReturnCorrectView()
    {
        //Arrange
        var productsController = new ProductsController(
            _productServiceMock.Object,
           _categoryServiceMock.Object,
           _supplierServiceMock.Object);

        //Act
        var result = await productsController.Delete(1);

        //Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.NotNull(redirectToActionResult);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public void BackToMainAction_ShouldRedirectToCorrectAction()
    {
        //Arrange
        var productsController = new ProductsController(
            _productServiceMock.Object,
           _categoryServiceMock.Object,
           _supplierServiceMock.Object);

        //Act
        var result = productsController.BackToMain();

        //Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.NotNull(redirectToActionResult);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }
}
