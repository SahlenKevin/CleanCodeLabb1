using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;

namespace WebShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<User>> GetUserByIdAsync(int userId)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] User user)
    {
        if (user == null)
            return BadRequest("User is null.");

        try
        {
            await _unitOfWork.Users.AddAsync(user);

            // Save changes
            await _unitOfWork.CompleteAsync();

            return Ok("Product added successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync(User user)
    {
        var existingUser = await _unitOfWork.Users.GetByIdAsync(user.Id);

        if(existingUser == null)
            return NotFound($"User with Id = {user.Id} not found.");

        try
        {
            existingUser.UserName = user.UserName;
            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            return Ok("User updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveUserAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user == null)
            return NotFound("User not found.");

        try
        {
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CompleteAsync();
            return Ok("User removed successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }
}