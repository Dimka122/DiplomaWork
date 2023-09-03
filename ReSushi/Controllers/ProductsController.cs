using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReSushi.Models;

namespace SushiStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public readonly EFDataContext _db;
        public ProductsController(EFDataContext db)
        {
            this._db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _db.Product.ToListAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product newProduct)
        {
           _db.Product.Add(newProduct);
            await _db.SaveChangesAsync();
            return Ok(newProduct);
        }


    }
}
        
