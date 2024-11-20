using Microsoft.EntityFrameworkCore;
using WebShop.Repository;

namespace WebShop.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WebShopDbContext _context;

    public UserRepository(WebShopDbContext context)
    {
        _context = context;
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void Update(User item)
    {
        throw new NotImplementedException();
    }

    public void Remove(User item)
    {
        throw new NotImplementedException();
    }
}