namespace ReSushi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ReSushi.interfaces;
    using ReSushi.Models.Pages;
    using ReSushi.Models;

    namespace SushiStore.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            private readonly ICategory _categories;

            public CategoriesController(ICategory categories)
            {
                _categories = categories;
            }

            [HttpGet("GetAll")]
            public IActionResult GetAllCategories()
            {
                var categories = _categories.GetAllCategories();
                return Ok(categories);
            }

            [HttpGet("GetCategories")]
            public IActionResult GetCategories([FromQuery] QueryOptions options)
            {
                var pagedCategories = _categories.GetCategories(options);
                return Ok(pagedCategories);
            }

            [HttpGet("GetById/{id}")]
            public IActionResult GetCategory(int id)
            {
                var category = _categories.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }

            [HttpPost("Add")]
            public IActionResult AddCategory(Category category)
            {
                _categories.AddCategory(category);
                return Ok(category);
            }

            [HttpPut("Update")]
            public IActionResult UpdateCategory(Category category)
            {
                _categories.UpdateCategory(category);
                return Ok(category);
            }

            [HttpDelete("Delete/{id}")]
            public IActionResult DeleteCategory(int id)
            {
                var category = _categories.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                _categories.DeleteCategory(category);
                return NoContent();
            }
        }
    }

}
