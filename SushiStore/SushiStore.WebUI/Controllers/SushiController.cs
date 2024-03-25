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
        public ViewResult List(string category,int page = 1)
        {
            SushisListViewModel model = new SushisListViewModel
            {
                Sushis = repository.Sushis
                .Where(p => category == null || p.Category == category)
                .OrderBy(Sushi => Sushi.SushiId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.Sushis.Count():
                    repository.Sushis.Where(sushi=>sushi.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
        public FileContentResult GetImage(int sushiId)
        {
            Sushi sushi = repository.Sushis
                .FirstOrDefault(g => g.SushiId == sushiId);

            if (sushi != null)
            {
                return File(sushi.ImageData, sushi.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}