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
        public IProductRepository Products { get; set; }
        public IUserRepository Users { get; set; }
        public IOrderRepository Orders { get; set; }


        public UnitOfWork(WebShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new ProductRepository(_context);
            Users = new UserRepository(_context);
            Orders = new OrderRepository(_context);
        }
        
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
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