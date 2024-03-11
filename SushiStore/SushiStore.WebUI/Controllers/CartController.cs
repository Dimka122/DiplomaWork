using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using SushiStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SushiStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ISushiRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(ISushiRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int sushiId, string returnUrl)
        {
            Sushi sushi = repository.Sushis
                .FirstOrDefault(g => g.SushiId == sushiId);

            if (sushi != null)
            {
                cart.AddItem(sushi, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int sushiId, string returnUrl)
        {
            Sushi sushi = repository.Sushis
                .FirstOrDefault(g => g.SushiId == sushiId);

            if (sushi != null)
            {
                cart.RemoveLine(sushi);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            return View(new ShippingDetails());
        }
    }
}