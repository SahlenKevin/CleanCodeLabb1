using Microsoft.EntityFrameworkCore;
using WebShop.Repository;

namespace WebShop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly WebShopDbContext _context;
    // private readonly DbSet<Product> _dbSet;
    public ProductRepository(WebShopDbContext context)
    {
        _context = context;
        // _dbSet = dbSet;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return null;
        }
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        await _context.AddAsync(product);
    }

    public Task UpdateAsync(Product item)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }
}