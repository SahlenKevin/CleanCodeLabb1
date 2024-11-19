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
    public async Task GetProduct_ReturnsOkResult_WithAProduct()
    {
        // Arrange
        var expectedProduct = new Product { Id = 1, Name = "TestProdukt" };

        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(expectedProduct.Id)).Returns(expectedProduct);
        // Act
        var result = await _controller.GetProductByIdAsync(expectedProduct.Id);
        
        // Assert
        var actionResult = Assert.IsType<ActionResult<Product>>(result);
        
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);
        
        var returnedProduct = Assert.IsAssignableFrom<Product>(okResult.Value);
        Assert.Equivalent(expectedProduct, returnedProduct);

        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(expectedProduct.Id)).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task GetProducts_ReturnsOkResult_WithAListOfProducts()
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

        A.CallTo(() => _unitOfWork.Products.GetAllAsync()).Returns(expectedProducts);

        // Act
        var result =  await _controller.GetProducts();

        // Assert 
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);

        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Equivalent(expectedProducts, returnedProducts);

        A.CallTo(() => _unitOfWork.Products.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void AddProduct_ReturnsOkResult()
    {
        // Arrange
        var testProduct = A.Fake<Product>();
        A.CallTo(() => _unitOfWork.Products.AddAsync(testProduct)).DoesNothing();

        // Act
        var result = _controller.AddProduct(testProduct);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        A.CallTo(() => _unitOfWork.Products.AddAsync(testProduct)).MustHaveHappenedOnceExactly();
    }
}