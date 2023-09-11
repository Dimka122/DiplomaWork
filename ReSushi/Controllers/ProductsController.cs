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
        public IEnumerable<Product> Get()

        {
            var products = _db.Products.Select(s => new Product
            {
                idProduct = s.idProduct,
                Name = s.Name,
                Price = s.Price,
                Detail = s.Detail,
                ImageUrl=s.ImageUrl,    
                idCategory = s.idCategory,
                Category = _db.Categories.Where(a => a.idCategory == s.idCategory).FirstOrDefault()
            }).ToList();
            return products;

            //var product=await _db.Products.ToListAsync();
            //return Ok(product);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var products = _db.Products.Select(s => new Product
            {
                idProduct = s.idProduct,
                Name = s.Name,
                Price = s.Price,
                Detail = s.Detail,
                ImageUrl=s.ImageUrl,
                idCategory = s.idCategory,
                Category = _db.Categories.Where(a => a.idCategory == s.idCategory).FirstOrDefault()
            }).Where(a => a.idProduct == id).FirstOrDefault();
            return products;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormProductView _product)
        {

            var product = new Product()
            {
                Name = _product.Name,
                Price = _product.Price,
                Detail = _product.Detail,
                ImageUrl = _product.ImageUrl,
                Category = _db.Categories.Find(_product.idCategory)
            };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            if (product.idProduct > 0)
            {
                return Ok(1);
            }
            return Ok(0);



        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FormProductView _user)
        {
            var product = _db.Products.Find(id);
             product.Name = _user.Name;
             product.Price = _user.Price;
             product.Detail = _user.Detail;
             product.ImageUrl = _user.ImageUrl;
             product.Category = _db.Categories.Find(_user.idCategory);
             await _db.SaveChangesAsync();
             return Ok(1);

            //_db.Products.Update(ProductToUpdate);
           // await _db.SaveChangesAsync();
           // return Ok(ProductToUpdate);

        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _db.Products.Find(id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(1);
        }



    }
}
        
