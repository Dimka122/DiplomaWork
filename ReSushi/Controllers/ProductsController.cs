using Microsoft.AspNetCore.Mvc;
using ReSushi.interfaces;
using ReSushi.Models;
using ReSushi.Models.Pages;

namespace ReSushi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _products;
        private readonly ICategory _categories;

        public ProductsController(IProduct products, ICategory categories)
        {
            _products = products;
            _categories = categories;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllProducts()
        {
            return Ok(_products.GetAllProducts());
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _products.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("CreateOrUpdate")]
        public IActionResult CreateOrUpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                _products.AddProduct(product);
            }
            else
            {
                _products.UpdateProduct(product);
            }
            return Ok(product);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            _products.DeleteProduct(product);
            return NoContent();
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts([FromQuery] QueryOptions options)
        {
            return Ok(_products.GetProducts(options));
        }
    }
}
