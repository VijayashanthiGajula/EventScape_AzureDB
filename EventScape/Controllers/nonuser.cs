using EventScape.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventScape.Controllers
{
    public class nonuser : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
