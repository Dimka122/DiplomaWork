
using Diploma___Work.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diploma___Work.Controllers
{
    public class SushiController: Controller
    {
        private readonly ApplicationDbContext _context;

        public SushiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Sushi> allSushi = _context.Sushi.ToList();
            return View(allSushi);
        }
    }
}
