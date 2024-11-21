using Microsoft.EntityFrameworkCore;
using WebShop.Repository;

namespace WebShop.Repositories;

public class UserRepository(WebShopDbContext context) : IUserRepository
{
    public async Task<User> GetByIdAsync(int id)
    {
       var user = await context.Users.FindAsync(id);
      
       if (user is null)
       {
           return null;
       }
       return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public void Update(User user)
    {
        context.Users.Update(user);
    }

    public void Remove(User user)
    {
        context.Users.Remove(user);
    }
}