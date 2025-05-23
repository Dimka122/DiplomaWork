﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using SushiStore.WebUI.Controllers;
using SushiStore.WebUI.Models;
using SushiStore.WebUI.HtmlHelpers;
using SushiStore.Domain.Concrete;

namespace SushiStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi>
            {
                new Sushi { SushiId = 1, Name = "Sushi1"},
                new Sushi { SushiId = 2, Name = "Sushi2"},
                new Sushi { SushiId = 3, Name = "Sushi3"},
                new Sushi { SushiId = 4, Name = "Sushi4"},
                new Sushi { SushiId = 5, Name = "Sushi5"}
            });
            SushiController controller = new SushiController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            SushisListViewModel result = (SushisListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            List<Sushi> sushis = result.Sushis.ToList();
            Assert.IsTrue(sushis.Count == 2);
            Assert.AreEqual(sushis[0].Name, "Sushi4");
            Assert.AreEqual(sushis[1].Name, "Sushi5");
        }
        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi>
            {
            new Sushi { SushiId = 1, Name = "Sushi1"},
            new Sushi { SushiId = 2, Name = "Sushi2"},
            new Sushi { SushiId = 3, Name = "Sushi3"},
            new Sushi { SushiId = 4, Name = "Sushi4"},
            new Sushi { SushiId = 5, Name = "Sushi5"}
            });
            SushiController controller = new SushiController(mock.Object);
            controller.pageSize = 3;

            // Act
            SushisListViewModel result
                = (SushisListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Sushi()
        {
            // Организация (arrange)
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi>
    {
        new Sushi { SushiId = 1, Name = "Sushi1", Category="Cat1"},
        new Sushi { SushiId = 2, Name = "Sushi2", Category="Cat2"},
        new Sushi { SushiId = 3, Name = "Sushi3", Category="Cat1"},
        new Sushi { SushiId = 4, Name = "Sushi4", Category="Cat2"},
        new Sushi { SushiId = 5, Name = "Sushi5", Category="Cat3"}
    });
            SushiController controller = new SushiController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Sushi> result = ((SushisListViewModel)controller.List("Cat2", 1).Model)
                .Games.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Sushi2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Sushi4" && result[1].Category == "Cat2");
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi> {
        new Sushi { SushiId = 1, Name = "Sushi1", Category="Суши"},
        new Sushi { SushiId = 2, Name = "Sushi2", Category="Суши"},
        new Sushi { SushiId = 3, Name = "Sushi3", Category="Ролл"},
        new Sushi { SushiId = 4, Name = "Sushi4", Category="Ассорти"},
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "Ассорти");
            Assert.AreEqual(results[1], "Суши");
            Assert.AreEqual(results[2], "Ролл");
        }
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new Sushi[] {
        new Sushi { SushiId = 1, Name = "Sushi1", Category="Суши"},
        new Sushi { SushiId = 2, Name = "Sushi2", Category="Ролл"}
    });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Ролл";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
        [TestMethod]
        public void Generate_Category_Specific_Sushi_Count()
        {
            /// Организация (arrange)
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi>
    {
        new Sushi { SushiId = 1, Name = "Sushi1", Category="Cat1"},
        new Sushi { SushiId = 2, Name = "Sushi2", Category = "Cat2"  },
        new Sushi { SushiId = 3, Name = "Sushi3", Category = "Cat1" },
        new Sushi { SushiId = 4, Name = "Sushi4", Category = "Cat2" },
        new Sushi { SushiId = 5, Name = "Sushi5", Category = "Cat3" }
    });
            SushiController controller = new SushiController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((SushisListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((SushisListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((SushisListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((SushisListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }

}
