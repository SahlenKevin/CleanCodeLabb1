using WebShop.Repositories;

namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
         // Repository för produkter
         IProductRepository Products { get; }
         IUserRepository Users { get; }
         Task<int> CompleteAsync();
         // Sparar förändringar (om du använder en databas)
         // Task SaveChangesAsync();
        void NotifyProductAdded(Product product); // Notifierar observatörer om ny produkt
    }
}

