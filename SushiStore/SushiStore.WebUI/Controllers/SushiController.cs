using SushiStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SushiStore.Domain.Entities;
using SushiStore.Domain.Abstract;
using SushiStore.WebUI.Models;

namespace SushiStore.WebUI.Controllers
{
    public class SushiController : Controller
    {
        private ISushiRepository repository;
        public int pageSize = 4;
        public SushiController(ISushiRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(int page = 1)
        {
            SushisListViewModel model = new SushisListViewModel
            {
            Sushis = repository.Sushis.OrderBy(Sushi => Sushi.SushiId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Sushis.Count()
                }
            };
            return View(model);
        }
    }
}