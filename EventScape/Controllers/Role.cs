using EventScape.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventScape.Controllers
{
    public class Role : Controller
    {
         

        public IActionResult Index()
        {
            return View();
        }
        //[Authorize(Policy = "RequireUser")]
        //[Authorize(Policy = "RequireAdmin")]

        [Authorize(Roles =$"{Constants.Roles.UserRole} ")]
        public IActionResult RequireUser()
        {
            return View();
        }
        //[Authorize(Policy = "RequireAdmin")]
        //[Authorize(Policy = "RequireNonUser")]
        //this is a manual work of assigning policies, which is a bad practice..-plocy based
        //So to automatize this feature I have created a core folder/contants and declared constants 

        [Authorize(Roles =$"{Constants.Roles.Administrator},{Constants.Roles.NonRegisteredUser}")]
        public IActionResult RequireNonUser()
        {
            return View();
        }
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.UserRole} ")]
        public IActionResult Admin()
        {
            return View();
        }

    }
}
