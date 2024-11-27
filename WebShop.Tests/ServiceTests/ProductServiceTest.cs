using FakeItEasy;
using WebShop;
using WebShop.Services;
using WebShop.UnitOfWork;

namespace WebShopTests.ServiceTests;

public class ProductServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductService _productService;

    public ProductServiceTest()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _productService = new ProductService(_unitOfWork);
    }

    [Fact]
    public async Task GetAllProducts_ReturnsListWithProducts()
    {
        // Arrange
        var allProducts = A.CollectionOfDummy<Product>(10);

        A.CallTo(() => _unitOfWork.Products.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Product>>(allProducts));
        
        // Act
        var result = await _productService.GetAllProducts();

        // Assert
        Assert.Equal(allProducts, result);
        A.CallTo(() => _unitOfWork.Products.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetProductById_WithValidInput_ReturnsProduct()
    {
        //Arrange
        var testProduct = A.Fake<Product>();

        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(testProduct.Id)).Returns(testProduct);
        // Act
        var result = await _productService.GetProductById(testProduct.Id);

        // Assert
        Assert.Equal(testProduct, result);
        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(testProduct.Id)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddNewProduct_WithValidInput_AddsProduct()
    {
        // Arrange
        var newProduct = A.Fake<Product>();

        A.CallTo(() => _unitOfWork.Products.AddAsync(newProduct)).DoesNothing();
        // Act
        await _productService.AddNewProduct(newProduct);
        
        // Assert
        A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Products.AddAsync(newProduct)).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task UpdateProductAsync_ProductExists_UpdatesProduct()
    {
        // Arrange
        var existingProduct = new Product { Id = 1, Name = "Old Name" };
        var updatedProduct = new Product { Id = 1, Name = "New Name" };
        
        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(existingProduct.Id))
            .Returns(Task.FromResult(existingProduct));

        // Act
        await _productService.UpdateProduct(updatedProduct);

        // Assert
        Assert.Equal("New Name", existingProduct.Name);
        A.CallTo(() => _unitOfWork.Products.Update(existingProduct)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task RemoveProduct_WithValidInput_RemovesProduct()
    {
        // Arrange
        var productToRemove = A.Fake<Product>();

        A.CallTo(() => _unitOfWork.Products.GetByIdAsync(productToRemove.Id)).Returns(productToRemove);
        A.CallTo(() => _unitOfWork.Products.Remove(productToRemove)).DoesNothing();
        
        // Act
        await _productService.RemoveProduct(productToRemove.Id);
        
        // Assert
        A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Products.Remove(productToRemove)).MustHaveHappenedOnceExactly();
    }
}