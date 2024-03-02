using Microsoft.AspNetCore.Mvc;
using Sushi.Models;

namespace Sushi.Controllers
{
    [Route("api/cartitems")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly VDbContext _context;

        public CartItemsController(VDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cartItems = _context.CartItems.ToList();
            return Ok(cartItems);
        }

        [HttpPost]
        public IActionResult PostCartItem([FromBody] CartItem cartItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.CartItems.Add(cartItem);
                    _context.SaveChanges();

                    return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.CartItemId }, cartItem);
                }

                return BadRequest("Validation error. Check the request data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message} \n Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCartItem(int id)
        {
            var cartItem = _context.CartItems.Find(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return Ok(cartItem);
        }
    }

}
