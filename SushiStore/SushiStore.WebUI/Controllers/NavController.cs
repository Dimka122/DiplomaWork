using SushiStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SushiStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private ISushiRepository repository;
        public NavController(ISushiRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu()
        {
            IEnumerable<string> categoryes = repository.Sushis
                .Select(s => s.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categoryes);
        }
    }
}