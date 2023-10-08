using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ReSushi.Models;
using ReSushi.Repository;
using System.IO;

namespace SushiStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        //private object _emp;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository??
                throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository??
                throw new ArgumentNullException(nameof(categoryRepository));
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productRepository.GetProducts());
        }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _productRepository.GetProductByID(Id));
        }
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> Post(Product prod)
        {
            var result = await _productRepository.InsertProduct(prod);
            if (result.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        //[Route("UpdateProduct")]
        
        public async Task<IActionResult> Put(Product prod)
        {
             await _productRepository.UpdateProduct(prod);
            return Ok("Updated Successfully");

           // _productRepository.UpdateProduct(prod);
            //await _productRepository.SaveChangesAsync();
           // return Ok(prod);
        }
        [HttpDelete]
        [Route("{Id:int}")]
        //[HttpDelete("{id}")]
        public JsonResult Delete(int Id)
        {
            var result = _productRepository.DeleteProduct(Id);
            return new JsonResult("Deleted Successfully");
        }

        //[Route("SaveFile")]
        //[HttpPost]
        //public JsonResult SaveFile()
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;
        //        var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

        //        using (var stream = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))

        //        {
        //            stream.CopyTo(stream);
        //        }

        //        return new JsonResult(filename);
        //    }
        //    catch (Exception)
        //    {
        //        return new JsonResult("anonymous.png");
        //    }
        //}

        [HttpGet]
        [Route("GetCategoryes")]
        public async Task<IActionResult> GetAllCategoryNames()
        {
            return Ok(await _categoryRepository.GetCategoryes());
        }
    }
}
        
