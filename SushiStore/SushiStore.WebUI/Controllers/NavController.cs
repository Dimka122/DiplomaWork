using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SushiStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        public string Menu()
        {
            return "Тестируем контроллер Nav";
        }
    }
}