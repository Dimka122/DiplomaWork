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
    }
}
