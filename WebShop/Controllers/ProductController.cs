using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // Endpoint f�r att h�mta alla produkter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Beh�ver anv�nda repository via Unit of Work f�r att h�mta produkter
            var products = await _unitOfWork.Products.GetAllAsync();
            
            return Ok(products);
        }

        // Endpoint f�r att l�gga till en ny produkt
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product is null.");

            try
            {
                _unitOfWork.Products.AddAsync(product);

                // Save changes
                _unitOfWork.CompleteAsync();

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
                var product = await _unitOfWork.Products.GetByIdAsync(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
