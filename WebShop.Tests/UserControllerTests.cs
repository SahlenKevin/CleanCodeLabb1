using System.Net;

namespace WebShopTests;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShop.UnitOfWork;

public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly IUnitOfWork _unitOfWork;

    public UserControllerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _controller = new UserController(_unitOfWork);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
    {
        // Arrange
        var expectedUsers = new List<User>
        {
            new User { Id = 1, UserName = "FirstUser" },
            new User { Id = 2, UserName = "SecondUser" }
        };

        A.CallTo(() => _unitOfWork.Users.GetAllAsync()).Returns(expectedUsers);
        // Act
        var result = await _controller.GetUsersAsync();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);

        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
        Assert.Equivalent(expectedUsers, returnedProducts);

        A.CallTo(() => _unitOfWork.Users.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddUser_ReturnsOkResult()
    {
        // Arrange
        var user = new User { Id = 1, UserName = "TestUser" };

        A.CallTo(() => _unitOfWork.Users.AddAsync(user)).DoesNothing();
        //Act
        var result = await _controller.AddUserAsync(user);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}