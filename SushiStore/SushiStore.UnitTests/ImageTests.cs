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
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Game с данными изображения
            Sushi sushi = new Sushi
            {
                SushiId = 2,
                Name = "Игра2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi> {
                new Sushi {SushiId = 1, Name = "Игра1"},
                sushi,
                new Sushi {SushiId = 3, Name = "Игра3"}
            }.AsQueryable());

            // Организация - создание контроллера
            SushiController controller = new SushiController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(sushi.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi> {
                new Sushi {SushiId = 1, Name = "Игра1"},
                new Sushi {SushiId = 2, Name = "Игра2"}
            }.AsQueryable());
            // Организация - создание контроллера
            SushiController controller = new SushiController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}
