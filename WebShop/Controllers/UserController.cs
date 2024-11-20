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
        var user = new User { Id = 1, UserName = "Kevin" };
        return Ok(user);
    }
}