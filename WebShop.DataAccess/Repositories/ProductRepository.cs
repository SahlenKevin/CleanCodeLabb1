using Microsoft.EntityFrameworkCore;
using WebShop.Repository;

namespace WebShop.Repositories;

public class ProductRepository(WebShopDbContext context) : IProductRepository
{
    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
        {
            return null;
        }
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        await context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        context.Products.Update(product);
    }

    public void Remove(Product product)
    {
        context.Products.Remove(product);
    }
}