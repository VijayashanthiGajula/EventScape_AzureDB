using EventScape.Areas.Identity.Data;
using EventScape.Core;
using EventScape.Core.Repository;
using EventScape.Core.Repository.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventScape.Controllers
{
    public class UserController : Controller
    {
        public readonly IUnitOfWork _UnitOfWork;
        public readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IUnitOfWork UnitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _UnitOfWork = UnitOfWork;//Repo from where user and roles data is brought from, by calling respective repos.
            _signInManager = signInManager; //injecting signmanager dependency to get list of userRoles of user
        }
       // [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Index()
        {
            var users = _UnitOfWork.User.GetUsers();
            return View(users);
        }
        //GET method
        public async Task<IActionResult> Edit(string id)
        {
            var user=  _UnitOfWork.User.GetUserById(id);
            var roles = _UnitOfWork.Role.GetRoles();
            var userRoles=await _signInManager.UserManager.GetRolesAsync(user);
            var roleSelectListItem = roles.Select(role => new SelectListItem(
                                                            role.Name,
                                                            role.Id,
                                                           userRoles.Any(userrole => userrole.Contains(role.Name))
                                                            )).ToList();
            var vm = new EditUserViewModel()
            {
                User = user,
                // Roles = roles
                Roles = roleSelectListItem
            };
           
            return View(vm);
           // return Content(user.LastName);
            //return Content(user.Email);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel vm)
        {
            return View(vm);

        }
    }
}
