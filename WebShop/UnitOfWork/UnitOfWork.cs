﻿using Microsoft.EntityFrameworkCore;
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
        
        public UnitOfWork(WebShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = new ProductRepository(_context);
            Users = new UserRepository(_context);
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