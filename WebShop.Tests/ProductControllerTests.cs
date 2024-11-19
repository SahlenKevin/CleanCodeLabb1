using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShop.UnitOfWork;

public class ProductControllerTests
{
    private readonly ProductController _controller;
    private readonly IUnitOfWork _unitOfWork;

    public ProductControllerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _controller = new ProductController(_unitOfWork);
        // Ställ in mock av Products-egenskapen
    }

    [Fact]
    public void GetProducts_ReturnsOkResult_WithAListOfProducts()
    {
        // Arrange
        List<Product> products = (List<Product>)A.CollectionOfDummy<Product>(1);
        
        // Act
        var result = _controller.GetProducts();
        
        // Assert
        
    }

    [Fact]
    public void AddProduct_ReturnsOkResult()
    {
        // Arrange
        var testProduct = A.Fake<Product>();
        A.CallTo(() => _unitOfWork.Products.Add(testProduct)).DoesNothing();    
        
        // Act
        var result = _controller.AddProduct(testProduct);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        A.CallTo(() => _unitOfWork.Products.Add(testProduct)).MustHaveHappenedOnceExactly();
    }
}
