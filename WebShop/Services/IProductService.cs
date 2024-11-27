namespace WebShop.Services;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetAllProducts();
    public Task<Product> GetProductById(int id);
    public Task AddNewProduct(Product product);
    public Task UpdateProduct(Product updatedProduct);
    public Task RemoveProduct(int productId);
}