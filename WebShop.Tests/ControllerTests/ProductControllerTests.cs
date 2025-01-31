using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShop.Services;
using WebShop.UnitOfWork;

namespace WebShopTests;

public class ProductControllerTests
{
    private readonly ProductController _controller;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductService _productService;

    public ProductControllerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _productService = A.Fake<IProductService>();
        _controller = new ProductController(_productService);
    }

    [Fact]
    public async void GetProduct_ReturnsOkResult_WithAProduct()
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
        
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equivalent(expectedProduct, returnedProduct);

        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(expectedProduct.Id)).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async void GetProducts_ReturnsOkResult_WithAListOfProducts()
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
        var result =  await _controller.GetProductsAsync();

        // Assert 
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);

        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Equivalent(expectedProducts, returnedProducts);

        A.CallTo(() => _unitOfWork.Products.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void AddProduct_ReturnsOkResult()
    {
        // Arrange
        var testProduct = A.Fake<Product>();
        A.CallTo(() => _unitOfWork.Products.AddAsync(testProduct)).DoesNothing();

        // Act
        var result = await _controller.AddProductAsync(testProduct);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        A.CallTo(() => _unitOfWork.Products.AddAsync(testProduct)).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async void AddProduct_ReturnsBadRequestResult()
    {
        // Arrange
        Product testProduct = null!;
        A.CallTo(() => _unitOfWork.Products.AddAsync(testProduct)).DoesNothing();

        // Act
        var result = await _controller.AddProductAsync(testProduct);
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        A.CallTo(() => _unitOfWork.Products.AddAsync(testProduct)).MustNotHaveHappened();
    }

    [Fact]
    public async void UpdateProduct_ReturnsOkResult()
    {
        // Arrange 
        var existingProduct = new Product { Id = 1, Name = "OldName" };
        var updatedProductName = new Product { Id = 1, Name = "NewName" };
        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(existingProduct.Id)).Returns(existingProduct);
        A.CallTo(() => _unitOfWork.Products.Update(existingProduct)).DoesNothing();
        
        // Act
        var result = await _controller.UpdateProductAsync(updatedProductName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(existingProduct.Id)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Products.Update(existingProduct)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async void RemoveProduct_ReturnsOkResult()
    {
        // Arrange
        var existingProduct = new Product { Id = 1, Name = "TestProduct" };
        
        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(existingProduct.Id)).Returns(existingProduct);
        A.CallTo(() => _unitOfWork.Products.Remove(existingProduct)).DoesNothing();

        // Act
        var result = await _controller.RemoveProductAsync(existingProduct.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        
        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(existingProduct.Id)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Products.Remove(existingProduct)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
    }

}