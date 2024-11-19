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
        // St�ll in mock av Products-egenskapen
    }

    [Fact]
    public void GetProducts_ReturnsOkResult_WithAListOfProducts()
    {
        // Arrange
        var expectedProducts = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "TestProdukt"
            },
            new Product
            {
                Id = 2,
                Name = "EnTillTestProdukt"
            },
        };

        A.CallTo(() => _unitOfWork.Products.GetAll()).Returns(expectedProducts);
        
        // Act
        var result = _controller.GetProducts();
        
        // Assert 
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);
        
        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value); 
        Assert.Equivalent(expectedProducts, returnedProducts);
        
        A.CallTo(() => _unitOfWork.Products.GetAll()).MustHaveHappenedOnceExactly();
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
