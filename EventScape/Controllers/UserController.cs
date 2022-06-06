using EventScape.Core.Repository;
using EventScape.Core.Repository.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventScape.Controllers
{
    public class UserController : Controller
    {
        public readonly IUnitOfWork _UnitOfWork;
        public UserController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            var users = _UnitOfWork.User.GetUsers();
            return View(users);
        }
        public IActionResult Edit(string id)
        {
            var user=  _UnitOfWork.User.GetUserById(id);
            var roles = _UnitOfWork.Role.GetRoles();
            var vm = new EditUserViewModel()
            {
                User = user,
                Roles = roles
            };
           
            return View(vm);
           // return Content(user.LastName);
            //return Content(user.Email);
        }
    }
}
