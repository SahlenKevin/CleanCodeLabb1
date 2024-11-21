namespace WebShop.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
}