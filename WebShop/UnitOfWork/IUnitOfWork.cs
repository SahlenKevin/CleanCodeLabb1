using WebShop.Repositories;

namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
         IProductRepository Products { get; }
         IUserRepository Users { get; }
         IOrderRepository Orders { get; }
         Task<int> CompleteAsync();
         // Sparar förändringar (om du använder en databas)
         // Task SaveChangesAsync();
        void NotifyProductAdded(Product product); // Notifierar observatörer om ny produkt
    }
}

