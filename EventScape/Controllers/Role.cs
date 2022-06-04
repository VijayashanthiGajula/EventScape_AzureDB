using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventScape.Controllers
{
    public class Role : Controller
    {
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
