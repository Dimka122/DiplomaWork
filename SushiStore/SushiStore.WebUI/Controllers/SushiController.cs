using SushiStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SushiStore.Domain.Entities;
using SushiStore.Domain.Abstract;

namespace SushiStore.WebUI.Controllers
{
    public class SushiController : Controller
    {
        private ISushiRepository repository;
        public SushiController(ISushiRepository repo)
        {
            repository = repo;
        }
        public ViewResult List()
        {
            return View(repository.Sushis);
        }
    }
}