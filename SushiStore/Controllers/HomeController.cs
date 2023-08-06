using Microsoft.AspNetCore.Mvc;
using SushiStore.Interfaces;
using SushiStore.Models;
using System.Diagnostics;

namespace SushiStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _products;

        public HomeController(IProduct products)
        {
            _products = products;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_products.GetAllProducts());
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            return View(id == 0 ? new Product() : _products.GetProduct(id));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                _products.AddProduct(product);
            }
            else
            {
                _products.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteProduct(Product product)
        {
            _products.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}