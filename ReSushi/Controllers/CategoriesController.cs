namespace ReSushi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ReSushi.Models;

    namespace SushiStore.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            public readonly EFDataContext _db;
            public CategoriesController(EFDataContext db)
            {
                this._db = db;
            }
            // GET: api/<CategoriesController>
            [HttpGet]
            public IEnumerable<Category> Get()
            {
                return _db.Categories.ToList();
            }

            // GET api/<CategoriesController>/5
            [HttpGet("{id}")]
            public Category Get(int id)
            {
                return _db.Categories.Find(id);
            }

            // POST api/<CategoriesController>
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] FormCategoryView _category)
            {
                var cate = new Category()
                {
                    Name = _category.Name,
                    Description = _category.Description
                };
                _db.Categories.Add(cate);
                await _db.SaveChangesAsync();
                if (cate.idCategory > 0)
                {
                    return Ok(1);
                }
                return Ok(0);
            }

            // PUT api/<CategoriesController>/5
            [HttpPut("{id}")]
            public void Put(int id)
            {
            }

            // DELETE api/<CategoriesController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }


        }
    }

}
