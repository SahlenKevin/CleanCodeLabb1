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
    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public void Add(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        _context.Add(product);
    }
}