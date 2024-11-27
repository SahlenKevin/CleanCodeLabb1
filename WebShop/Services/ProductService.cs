using WebShop.UnitOfWork;

namespace WebShop.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await unitOfWork.Products.GetAllAsync();
        return products;
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} was not found.");
        }
        
        return product;
    }

    public async Task AddNewProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product), "Product is null.");

        await unitOfWork.Products.AddAsync(product);
        await unitOfWork.CompleteAsync();
    }

    public async Task UpdateProduct(Product updatedProduct)
    {
        if (updatedProduct == null)
        {
            throw new ArgumentNullException(nameof(updatedProduct), "Updated product is null.");
        }

        var existingProduct = await unitOfWork.Products.GetByIdAsync(updatedProduct.Id);

        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {updatedProduct.Id} was not found.");
        }

        existingProduct.Name = updatedProduct.Name;

        unitOfWork.Products.Update(existingProduct);
        await unitOfWork.CompleteAsync();
    }

    public async Task RemoveProduct(int productId)
    {
        var product = await unitOfWork.Products.GetByIdAsync(productId);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} was not found.");
        }

        unitOfWork.Products.Remove(product);
        await unitOfWork.CompleteAsync();
    }
    
}