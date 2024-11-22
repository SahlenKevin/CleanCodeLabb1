using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await unitOfWork.Products.GetAllAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product is null.");

            try
            {
                await unitOfWork.Products.AddAsync(product);

                // Save changes
                await unitOfWork.CompleteAsync();
                unitOfWork.NotifyProductAdded(product);
                return Ok("Product added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await unitOfWork.Products.GetByIdAsync(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(Product updatedProduct)
        {
            var product = await unitOfWork.Products.GetByIdAsync(updatedProduct.Id);

            if (product == null)
                return BadRequest("Product is null.");

            try
            {
                product.Name = updatedProduct.Name;

                unitOfWork.Products.Update(product);
                await unitOfWork.CompleteAsync();

                return Ok("Product update successful");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProductAsync(int productId)
        {
            var product = await unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
                return NotFound("Product not found.");

            try
            {
                unitOfWork.Products.Remove(product);
                await unitOfWork.CompleteAsync();

                return Ok("Product removed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}