using Microsoft.AspNetCore.Mvc;
using ReSushi.interfaces;
using ReSushi.Models.Pages;
using ReSushi.Models;

namespace SushiStore.Controllers
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

        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            return Ok(_categories.GetAllCategories());
        }

        [HttpGet("Update/{id}")]
        public IActionResult UpdateProduct(int id)
        {
            var product = id == 0 ? new Product() : _products.GetProduct(id);
            var categories = _categories.GetAllCategories();
            return Ok(new { Product = product, Categories = categories });
        }

        [HttpPost("AddOrUpdate")]
        public IActionResult AddOrUpdateProduct(Product product)
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
            return Ok();
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts([FromQuery] QueryOptions options)
        {
            return Ok(_products.GetProducts(options));
        }
    }
}

