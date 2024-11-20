namespace WebShop.Repositories;

public class UserRepository: IUserRepository
{
    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(User item)
    {
        throw new NotImplementedException();
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