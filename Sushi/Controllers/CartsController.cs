using Microsoft.AspNetCore.Mvc;
using Sushi.Models;

namespace Sushi.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly VDbContext _context;

        public CartsController(VDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCarts()
        {
            var carts = _context.Carts.ToList();
            return Ok(carts);
        }

        [HttpPost]
        public IActionResult PostCart([FromBody] Cart cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Carts.Add(cart);
                    _context.SaveChanges();

                    return CreatedAtAction(nameof(GetCart), new { id = cart.CartId }, cart);
                }

                return BadRequest("Validation error. Check the request data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message} \n Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCart(int id)
        {
            var cart = _context.Carts.Find(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }
    }

}
