using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventScape.Controllers
{
    public class Role : Controller
    {
        [Authorize(Policy = "RequireAdmin")]
        [Authorize(Policy = "RequireUser")]
        [Authorize(Policy = "RequireNonUser")]

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "RequireUser")]
        [Authorize(Policy = "RequireAdmin")]
        public IActionResult RequireUser()
        {
            return View();
        }
        [Authorize(Policy = "RequireAdmin")]
        [Authorize(Policy = "RequireNonUser")]
        public IActionResult RequireNonUser()
        {
            return View();
        }
        public IActionResult testVijaya()
        {
            return View();
        }

    }
}
