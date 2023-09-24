using Microsoft.AspNetCore.Mvc;
using ReSushi.Models;

namespace ReSushi.Controllers
{
    namespace SushiStore.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            public readonly EFDataContext _db;
        }
    }

}
