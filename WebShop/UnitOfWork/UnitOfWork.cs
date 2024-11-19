using Microsoft.EntityFrameworkCore;
using WebShop.Notifications;
using WebShop.Repositories;
using WebShop.Repository;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        // Hämta produkter från repository
        private readonly WebShopDbContext _context;
        private readonly DbSet<Product> _dbSet;
        public IProductRepository Products { get; set; }
        
        public UnitOfWork(WebShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new ProductRepository(_context, _dbSet);
        }

        public Task<IProductRepository> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void NotifyProductAdded(Product product)
        {
            // _productSubject.Notify(product);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}


// private readonly ProductSubject _productSubject;
//
// // Konstruktor används för tillfället av Observer pattern
// public UnitOfWork(ProductSubject productSubject = null)
// {
//     Products = null;
//
//     // Om inget ProductSubject injiceras, skapa ett nytt
//     _productSubject = productSubject ?? new ProductSubject();
//
//     // Registrera standardobservatörer
//     _productSubject.Attach(new EmailNotification());
// }