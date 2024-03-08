using System;
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
            SushisListViewModel result = (SushisListViewModel)controller.List(2).Model;

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
                = (SushisListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
