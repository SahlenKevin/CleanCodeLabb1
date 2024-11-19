using FakeItEasy;
using WebShop;
using WebShop.Controllers;
using WebShop.Repositories;
using WebShop.UnitOfWork;

public class ProductControllerTests
{
    private readonly IProductRepository _mockProductRepository;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        _mockProductRepository = A.Fake<IProductRepository>();
        _controller = new ProductController(unitOfWork);
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

        // Act

        // Assert
    }
}
