using WebShop.Repositories;

namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork
    {
         // Repository för produkter
         Task<IProductRepository> GetProductsAsync();
         // Sparar förändringar (om du använder en databas)
         Task SaveChangesAsync();
        void NotifyProductAdded(Product product); // Notifierar observatörer om ny produkt
    }
}

