﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SushiStore.WebUI.Controllers;
using SushiStore.WebUI.Infrastructure.Abstract;
using SushiStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SushiStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Организация - создание имитации поставщика аутентификации
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "12345")).Returns(true);

            // Организация - создание модели представления
            // с правильными учетными данными
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "12345"
            };

            // Организация - создание контроллера
            AccountController target = new AccountController(mock.Object);

            // Действие - аутентификация
            ActionResult result = target.Login(model, "/MyURL");

            // Утверждение
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // Организация - создание имитации поставщика аутентификации
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            // Организация - создание модели представления
            // с некорректными учетными данными
            LoginViewModel model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };

            // Организация - создание контроллера
            AccountController target = new AccountController(mock.Object);

            // Действие - аутентификация
            ActionResult result = target.Login(model, "/MyURL");

            // Утверждение
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
