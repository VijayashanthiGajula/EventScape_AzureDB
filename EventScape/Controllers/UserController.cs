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
            var UserData = new EditUserViewModel()
            {
                User = user,
                // Roles = roles
                Roles = roleSelectListItem
            };
           
            return View(UserData);
           // return Content(user.LastName);
            //return Content(user.Email);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel UserData)
        {
            var user = _UnitOfWork.User.GetUserById(UserData.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);
            //assigning userrole data
            //foreach (var role in UserData.Roles)
            //{
            //    var assignedInDb = userRolesInDb.FirstOrDefault(Ur => Ur == role.Text);
            //    if (role.Selected)
            //    {
            //        if (assignedInDb == null)
            //        {
            //            await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
            //           //AddRole

            //        }
            //    }
            //    else
            //    {
            //        if (assignedInDb != null)
            //        {
            //            //RemoveRole
            //            await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
            //        }
            //    }

            //}

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in UserData.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            //assign user updated data
            user.FirstName = UserData.User.FirstName;
            user.LastName = UserData.User.LastName; 
            user.Email = UserData.User.Email;   
            _UnitOfWork.User.UpdateUser(user);
            return RedirectToAction("Edit",new { id = user.Id });   

        }
    }
}
