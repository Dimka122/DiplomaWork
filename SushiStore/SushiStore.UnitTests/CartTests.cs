using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using SushiStore.WebUI.Controllers;
using SushiStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static SushiStore.Domain.Entities.Cart;


namespace SushiStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
       public void Can_Add_New_Lines()
       {
            Sushi sushi1 = new Sushi { SushiId = 1, Name = "Sushi1" };
            Sushi sushi2 = new Sushi { SushiId = 2, Name = "Sushi2" };

            Cart cart = new Cart();

            cart.AddItem(sushi1, 1);
            cart.AddItem(sushi2, 1);
            List<CartLine> results = cart.Lines.ToList();
            Assert.AreEqual( results.Count(),2);
            Assert.AreEqual(results[0].Sushi, sushi1);
            Assert.AreEqual(results[1].Sushi,sushi2);
       }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        { 
         
            Sushi sushi1 = new Sushi { SushiId = 1, Name = "Sushi1" };
            Sushi sushi2 = new Sushi { SushiId = 2, Name = "Sushi2" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(sushi1, 1);
            cart.AddItem(sushi2, 1);
            cart.AddItem(sushi1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Sushi.SushiId).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            // Организация - создание нескольких тестовых sushi
            Sushi sushi1 = new Sushi { SushiId = 1, Name = "Sushi1" };
            Sushi sushi2 = new Sushi { SushiId = 2, Name = "Sushi2" };
            Sushi sushi3 = new Sushi { SushiId = 3, Name = "Sushi3" };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - добавление нескольких sushi в корзину
            cart.AddItem(sushi1, 1);
            cart.AddItem(sushi2, 4);
            cart.AddItem(sushi3, 2);
            cart.AddItem(sushi2, 1);

            // Действие
            cart.RemoveLine(sushi2);

            // Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Sushi == sushi2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Организация - создание нескольких тестовых sushi
            Sushi sushi1 = new Sushi { SushiId = 1, Name = "Sushi1", Price = 100 };
            Sushi sushi2 = new Sushi { SushiId = 2, Name = "Sushi2", Price = 55 };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(sushi1, 1);
            cart.AddItem(sushi2, 1);
            cart.AddItem(sushi1, 5);
            decimal result = cart.ComputeTotalValue();

            // Утверждение
            Assert.AreEqual(result, 655);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Организация - создание нескольких тестовых sushi
            Sushi sushi1 = new Sushi { SushiId = 1, Name = "Sushi1", Price = 100 };
            Sushi sushi2 = new Sushi { SushiId = 2, Name = "Sushi2", Price = 55 };

            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(sushi1, 1);
            cart.AddItem(sushi2, 1);
            cart.AddItem(sushi1, 5);
            cart.Clear();

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        /// <summary>
        /// Проверяем добавление в корзину
        /// </summary>
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Организация - создание имитированного хранилища
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi> {
        new Sushi {SushiId = 1, Name = "Sushi1", Category = "Кат1"},
    }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object);

            // Действие - добавить игру в корзину
            controller.AddToCart(cart, 1, null);

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Sushi.SushiId, 1);
        }

        /// <summary>
        /// После добавления игры в корзину, должно быть перенаправление на страницу корзины
        /// </summary>
        [TestMethod]
        public void Adding_Game_To_Cart_Goes_To_Cart_Screen()
        {
            // Организация - создание имитированного хранилища
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi> {
        new Sushi {SushiId = 1, Name = "Sushi1", Category = "Кат1"},
    }.AsQueryable());

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object);

            // Действие - добавить игру в корзину
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            // Утверждение
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        // Проверяем URL
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController target = new CartController(null);

            // Действие - вызов метода действия Index()
            CartIndexViewModel result
                = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // Утверждение
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
