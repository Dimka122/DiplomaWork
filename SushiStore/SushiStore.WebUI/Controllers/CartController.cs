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
        public CartController(ISushiRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int sushiId, string returnUrl)
        {
            Sushi sushi = repository.Sushis
                .FirstOrDefault(g => g.SushiId == sushiId);

            if (sushi != null)
            {
                GetCart().AddItem(sushi, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int sushiId, string returnUrl)
        {
            Sushi sushi = repository.Sushis
                .FirstOrDefault(g => g.SushiId == sushiId);

            if (sushi != null)
            {
                GetCart().RemoveLine(sushi);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

    }
}