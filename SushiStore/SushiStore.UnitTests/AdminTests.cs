using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using SushiStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SushiStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Sushis()
        {
            Mock<ISushiRepository> mock= new Mock<ISushiRepository>();
            mock.Setup(m=>m.Sushis).Returns(new List<Sushi>
            {
                new Sushi { SushiId=1,Name="Sushi1"},
                new Sushi { SushiId=2,Name="Sushi2"},
                new Sushi { SushiId=3,Name="Sushi3"},
                new Sushi {SushiId=4,Name="Sushi4"},
                new Sushi {SushiId=5,Name="Sushi5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Sushi> result = ((IEnumerable<Sushi>)controller.Index().
                ViewData.Model).ToList();
            Assert.AreEqual( result.Count(),5);
            Assert.AreEqual("Sushi1", result[0].Name);
            Assert.AreEqual("Sushi2", result[1].Name);
            Assert.AreEqual("Sushi3", result[2].Name);

        }

        [TestMethod]
        public void Can_Edit_Sushi()
        {
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi>
            {
                new Sushi { SushiId=1,Name="Sushi1"},
                new Sushi { SushiId=2,Name="Sushi2"},
                new Sushi { SushiId=3,Name="Sushi3"},
                new Sushi {SushiId=4,Name="Sushi4"},
                new Sushi {SushiId=5,Name="Sushi5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            Sushi sushi1= controller.Edit(1).ViewData.Model as Sushi;
            Sushi sushi2 = controller.Edit(2).ViewData.Model as Sushi;
            Sushi sushi3 = controller.Edit(3).ViewData.Model as Sushi;

            Assert.AreEqual(1, sushi1.SushiId);
            Assert.AreEqual(2, sushi2.SushiId);
            Assert.AreEqual(3, sushi3.SushiId);
        }
        public void Cannot_Edit_Nonexistent_Sushi()
        {
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi>
            {
                new Sushi { SushiId=1,Name="Sushi1"},
                new Sushi { SushiId=2,Name="Sushi2"},
                new Sushi { SushiId=3,Name="Sushi3"},
                new Sushi {SushiId=4,Name="Sushi4"},
                new Sushi {SushiId=5,Name="Sushi5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            Sushi result=controller.Edit(6).ViewData.Model as Sushi;
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта 
            Sushi sushi = new Sushi { Name = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(sushi);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveSushi(sushi));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта 
            Sushi sushi = new Sushi { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(sushi);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveSushi(It.IsAny<Sushi>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Sushis()
        {
            Sushi sushi = new Sushi { SushiId = 2, Name = "Sushi2" };
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi>
            {
                new Sushi { SushiId=1,Name="Sushi1"},
                new Sushi { SushiId=2,Name="Sushi2"},
                new Sushi { SushiId=3,Name="Sushi3"},
                new Sushi {SushiId=4,Name="Sushi4"},
                new Sushi {SushiId=5,Name="Sushi5"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            controller.Delete(sushi.SushiId);

            mock.Verify(m=>m.DeleteSushi(sushi.SushiId));
        }
    }
}
