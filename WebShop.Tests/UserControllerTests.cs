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
    public async Task GetUser_ReturnsOkResult_WithAUser()
    {
        // Arrange
        var expectedUser = new User { Id = 1, UserName = "TestUser" };

        A.CallTo(() => _unitOfWork.Users.GetByIdAsync(expectedUser.Id)).Returns(expectedUser);
        // Act
        var result = await _controller.GetUserByIdAsync(expectedUser.Id);
        
        // Assert
        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);
        
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equivalent(expectedUser, returnedUser);

        A.CallTo(() => _unitOfWork.Users.GetByIdAsync(expectedUser.Id)).MustHaveHappenedOnceExactly();
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

    [Fact]
    public async Task UpdateUser_ReturnsOkResult()
    {
        // Arrange
        var user = new User { Id = 1, UserName = "TestUser" };

        A.CallTo(() => _unitOfWork.Users.Update(user)).DoesNothing();
        //Act
        var result = await _controller.UpdateUserAsync(user);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNotFoundResult_WhenUserNotFound()
    {
        // Arrange
        var user = new User { Id = 1, UserName = "TestUser" };

        A.CallTo(() => _unitOfWork.Users.GetByIdAsync(user.Id))!.Returns((User)null);
        //Act
        var result = await _controller.UpdateUserAsync(user);
        
        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task RemoveUser_ReturnsOkResult()
    {
        // Arrange
        var user = new User { Id = 1, UserName = "TestUser" };

        A.CallTo(() => _unitOfWork.Users.GetByIdAsync(user.Id)).Returns(user);
        A.CallTo(() => _unitOfWork.Users.Remove(user)).DoesNothing();
        //Act
        var result = await _controller.RemoveUserAsync(user.Id);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);

        A.CallTo(() => _unitOfWork.Users.GetByIdAsync(user.Id)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.Users.Remove(user)).MustHaveHappenedOnceExactly();
    }
}