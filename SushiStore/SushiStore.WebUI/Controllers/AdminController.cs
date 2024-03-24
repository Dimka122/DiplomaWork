using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SushiStore.WebUI.Controllers
{
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

        public ViewResult Edit(int sushisId)
        {
            Sushi sushi= repository.Sushis.FirstOrDefault(s=>s.SushiId==sushisId);
            return View(sushi);
        }
    }
}