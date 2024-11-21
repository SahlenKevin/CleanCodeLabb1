using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShop.UnitOfWork;

namespace WebShopTests;

public class OrderControllerTests
{
    private readonly OrderController _controller;
    private readonly IUnitOfWork _unitOfWork;

    public OrderControllerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _controller = new OrderController(_unitOfWork);
    }

    [Fact]
    public async Task GetOrderById_ReturnsOkResult_WithAOrder()
    {
        // Arrange
        var expectedOrder = new Order
        {
            Id = 1,
            UserId = 1,
            Products = new List<OrderProduct>
            {
                new OrderProduct
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 2
                }
            }
        };

        A.CallTo(() => _unitOfWork.Orders.GetByIdAsync(expectedOrder.Id)).Returns(expectedOrder);

        // Act
        var result = await _controller.GetOrderByIdAsync(expectedOrder.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Order>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(expectedOrder, okResult.Value);

        A.CallTo(() => _unitOfWork.Orders.GetByIdAsync(expectedOrder.Id)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetOrderByUserId_ReturnsOkResult_WithListOfOrder()
    {
        // Arrange
        var expectedOrders = new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            },
            new Order
            {
                Id = 2,
                UserId = 1,
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        OrderId = 2,
                        ProductId = 2,
                        Quantity = 3
                    }
                }
            }
        };

        A.CallTo(() => _unitOfWork.Orders.GetByUserIdAsync(1)).Returns(expectedOrders);

        // Act
        var result = await _controller.GetOrderByUserIdAsync(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Order>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(expectedOrders, okResult.Value);

        A.CallTo(() => _unitOfWork.Orders.GetByUserIdAsync(1)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetAllOrders_ReturnsOkResult_WithAListOfOrder()
    {
        // Arrange
        var expectedOrders = new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            },
            new Order
            {
                Id = 2,
                UserId = 2,
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        OrderId = 2,
                        ProductId = 2,
                        Quantity = 3
                    }
                }
            }
        };

        A.CallTo(() => _unitOfWork.Orders.GetAllAsync()).Returns(expectedOrders);

        // Act
        var result = await _controller.GetAllOrdersAsync();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Order>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(expectedOrders, okResult.Value);

        A.CallTo(() => _unitOfWork.Orders.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddOrder_GetOkResult()
    {
        // Arrange
        var testUser = new User { Id = 1, UserName = "TestUser" };
        var testOrder = new Order
        {
            Id = 1,
            UserId = testUser.Id,
            Products = [],
        };

        var testProduct = new Product { Id = 1, Name = "TestProduct", Price = 500, Stock = 5 };

        var product = new OrderProduct
        {
            OrderId = testOrder.Id,
            ProductId = testProduct.Id,
            Quantity = 2
        };

        testOrder.Products.Add(product);

        A.CallTo(() => _unitOfWork.Orders.AddAsync(testOrder)).DoesNothing();

        // Act
        var result = await _controller.CreateOrderAsync(testOrder);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        A.CallTo(() => _unitOfWork.Orders.AddAsync(testOrder)).MustHaveHappenedOnceExactly();
    }

}
