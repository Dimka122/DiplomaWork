using Microsoft.AspNetCore.Mvc;
using ReSushi.Models;
using ReSushi.Repository;
using System.Security.Cryptography.X509Certificates;

namespace ReSushi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));

            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(_orderRepository));
        }
            [HttpGet]
            [Route("GetOrders")]

            public async Task<IActionResult> GetOrders()
            {
                return Ok(await _orderRepository.GetOrders());
            }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _orderRepository.GetOrderByID(Id));
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> Post(Order order)
        {
            var result = await _orderRepository.InsertOrder(order);
            if (result.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        public async Task<IActionResult> Put(Order order)
        {
            await _orderRepository.UpdateOrder(order);
            return Ok("Updated Successfully");
        }
        
        [HttpDelete]
        [Route("{Id:int}")]
        public JsonResult Delete(int Id)
        {
            var result = _orderRepository.DeleteOrder(Id);
            return new JsonResult("Deleted Successfully");
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productRepository.GetProducts());
        }

    }
}
