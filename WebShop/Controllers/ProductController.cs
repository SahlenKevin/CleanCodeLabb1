using Microsoft.AspNetCore.Mvc;
using WebShop.Services;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await productService.GetAllProducts();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            try
            {
                await productService.AddNewProduct(product);
                return Ok("Product added successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
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
                var product = await productService.GetProductById(productId);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(Product updatedProduct)
        {
            try
            {
                await productService.UpdateProduct(updatedProduct);
                return Ok("Product update successful.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProductAsync(int productId)
        {
            try
            {
                await productService.RemoveProduct(productId);
                return Ok("Product removed successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}