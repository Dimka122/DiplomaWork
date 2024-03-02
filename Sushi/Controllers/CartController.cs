using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sushi.Models;

namespace Sushi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly VDbContext _context;

        public CartController(VDbContext context)
        {
            _context = context;
        }

        // GET: api/cart/{id}
        [HttpGet("{id}")]
        public IActionResult GetCart(int id)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // POST: api/cart/{id}/add-to-cart
        [HttpPost("{id}/add-to-cart")]
        public IActionResult AddToCart(int id, [FromBody] CartItem cartItem)
        {
            var cart = _context.Carts.Find(id);

            if (cart == null)
            {
                return NotFound();
            }

            var product = _context.Products.Find(cartItem.ProductId);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            cart.CartItems.Add(cartItem);
            _context.SaveChanges();

            return Ok(cart);
        }
    }

}
