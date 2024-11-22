using Microsoft.EntityFrameworkCore;
using WebShop.Notifications;
using WebShop.Repositories;
using WebShop.Repository;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebShopDbContext _context;
        
        private readonly ProductSubject _productSubject;
        public IProductRepository Products { get; set; }
        public IUserRepository Users { get; set; }
        public IOrderRepository Orders { get; set; }


        public UnitOfWork(WebShopDbContext context, IOrderRepository orderRepository,
            IProductRepository productRepository, IUserRepository userRepository, ProductSubject productSubject)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = productRepository;
            Users = userRepository;
            Orders = orderRepository;

            _productSubject = productSubject;
            _productSubject.Attach(new EmailNotification());
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
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