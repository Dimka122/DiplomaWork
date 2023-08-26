namespace ReSushi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ReSushi.interfaces;
    using ReSushi.Models;

    namespace SushiStore.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class OrdersController : ControllerBase
        {
            private readonly IProduct _products;
            private readonly IOrder _orders;

            public OrdersController(IProduct products, IOrder orders)
            {
                _products = products;
                _orders = orders;
            }

            [HttpGet]
            [Route("GetAll")]
            public IActionResult GetAllOrders()
            {
                return Ok(_orders.GetAllOrders());
            }

            [HttpGet]
            [Route("Edit/{id}")]
            public IActionResult EditOrder(int id)
            {
                var products = _products.GetAllProducts();
                var order = id == 0 ? new Order() : _orders.GetOrder(id);
                // Ваша логика для получения списка товаров и обработки заказа
                return Ok(order);
            }

            [HttpPost]
            [Route("AddOrUpdate")]
            public IActionResult AddOrUpdateOrder(Order order)
            {
                order.Lines = order.Lines.Where(e => e.Id > 0 || (e.Id == 0 && e.Quantity > 0)).ToArray();
                if (order.Id == 0)
                {
                    _orders.AddOrder(order);
                }
                else
                {
                    _orders.UpdateOrder(order);
                }
                return Ok(order);
            }

            [HttpDelete]
            [Route("Delete/{id}")]
            public IActionResult DeleteOrder(int id)
            {
                var order = _orders.GetOrder(id);
                if (order == null)
                {
                    return NotFound();
                }

                _orders.DeleteOrder(order);
                return Ok();
            }
        }
    }

}
