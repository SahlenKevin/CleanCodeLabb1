using Microsoft.EntityFrameworkCore;
using WebShop.Repository;

namespace WebShop.Repositories;

public class OrderRepository(WebShopDbContext context) : IOrderRepository
{
    public async Task<Order> GetByIdAsync(int orderId)
    {
        var order = await context.Orders
            .Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null)
        {
            return null;
        }
        return order;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orders = await context.Orders
            .Include(o => o.Products)
            .ToListAsync();
        return orders;
    }

    public async Task AddAsync(Order order)
    {
        await context.Orders.AddAsync(order);
    }

    public void Update(Order order)
    {
        throw new NotImplementedException();
    }

    public void Remove(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        var orders = await context.Orders.Where(o => o.UserId == userId)
            .Include(o => o.Products)
            .ToListAsync();

        return orders;
    }
}