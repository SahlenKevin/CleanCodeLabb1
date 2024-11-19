namespace WebShop.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task RemoveAsync(int id);
}