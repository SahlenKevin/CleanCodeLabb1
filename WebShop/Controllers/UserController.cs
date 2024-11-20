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
}