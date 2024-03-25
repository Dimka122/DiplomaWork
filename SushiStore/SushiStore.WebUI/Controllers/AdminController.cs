using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SushiStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ISushiRepository repository;

        public AdminController(ISushiRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            return View(repository.Sushis);
        }

        public ViewResult Edit(int sushiId)
        {
            Sushi sushi= repository.Sushis.FirstOrDefault(s=>s.SushiId==sushiId);
            return View(sushi);
        }
        [HttpPost]
        public ActionResult Edit(Sushi sushi, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    sushi.ImageMimeType=image.ContentType;
                    sushi.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(sushi.ImageData, 0, image.ContentLength);
                }
                    repository.SaveSushi(sushi);
                TempData["message"] = string.Format("Изменения в продукте \"{0}\" были сохранены", sushi.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(sushi);
            }
        }

        public ViewResult Create()
        {
            return View("Edit",new Sushi());
        }

        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            Sushi deletedSushi=repository.DeleteSushi(gameId);
            if (deletedSushi != null)
            {
                TempData["message"]=String.Format("продукт \"{0}\" был удален",
                    deletedSushi.Name);
            }
            return RedirectToAction("Index");
        }
    }
}