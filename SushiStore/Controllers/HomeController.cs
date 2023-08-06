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
            System.Console.Clear();
            return View(_products.GetAllProducts());
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _products.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            return View(_products.GetProduct(id));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            _products.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}