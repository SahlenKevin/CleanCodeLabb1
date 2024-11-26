using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;

namespace WebShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersAsync()
    {
        var orders = await unitOfWork.Orders.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<Order>> GetOrderByIdAsync(int orderId)
    {
        try
        {
            var order = await unitOfWork.Orders.GetByIdAsync(orderId);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserIdAsync(int userId)
    {
        return Ok(await unitOfWork.Orders.GetByUserIdAsync(userId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(Order order)
    {
        if (order == null)
            return BadRequest("Order is null.");

        try
        {
            await unitOfWork.Orders.AddAsync(order);
            await unitOfWork.CompleteAsync();

            return Ok("Order added successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}