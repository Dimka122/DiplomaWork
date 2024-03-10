using Microsoft.VisualStudio.TestTools.UnitTesting;
using SushiStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
//using SushiStore.Domain.Entities.Cart;
//using SushiStore.Domain.Entities;

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
    }
}
