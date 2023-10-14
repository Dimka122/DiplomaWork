using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReSushi.Models;
using ReSushi.Repository;

namespace ReSushi.Controllers
{
    namespace SushiStore.Controllers
    {
       
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            private readonly ICategoryRepository _categoryRepository;
            public CategoriesController(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository??
                    throw new ArgumentNullException(nameof(categoryRepository));

            }
            [HttpGet]
            [Route("GetCategories")]
            public async Task<IActionResult> Get()
            {
                return Ok(await _categoryRepository.GetCategoryes());
            }
            [HttpGet]
            [Route("GetCategoryByID/{Id}")]
            public async Task<IActionResult> GetDeptById(int Id)
            {
                return Ok(await _categoryRepository.GetCategoryByID(Id));
            }
            [HttpPost]
            [Route("AddCategory")]
            public async Task<IActionResult> Post(Category cat)
            {
                var result = await _categoryRepository.InsertCategory(cat);
                if (result.Id == 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }
                return Ok("Added Successfully");
            }
            [HttpPut]
            [Route("UpdateCategory")]
            public async Task<IActionResult> Put(Category cat)
            {
                await _categoryRepository.UpdateCategory(cat);
                return Ok("Updated Successfully");
            }
            [HttpDelete]
            //[HttpDelete("{id}")]
            [Route("DeleteCategory")]
            public JsonResult Delete(int id)
            {
                _categoryRepository.DeleteCategory(id);
                return new JsonResult("Deleted Successfully");
            }
        }
    }

}
