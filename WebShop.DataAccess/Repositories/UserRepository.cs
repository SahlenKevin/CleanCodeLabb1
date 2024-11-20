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

    public async Task<User> GetByIdAsync(int id)
    {
       var user = await _context.Users.FindAsync(id);
      
       if (user is null)
       {
           return null;
       }
       return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}