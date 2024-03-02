using Microsoft.AspNetCore.Mvc;
using Sushi.Models;

namespace Sushi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly VDbContext _context;

        public ProductsController(VDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();

                    return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
                }

                return BadRequest("Validation error. Check the request data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message} \n Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }

}
