using EventScape.Areas.Identity.Data;
using EventScape.Data;
using EventScape.Models;
using EventScape.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace EventScape.Controllers
{
    public class AdminDashboardController : Controller
    {

        private readonly ApplicationDbContext _context;
        public AdminDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // ViewData["UpcomingEvents"] =eventslist;
           // var eventsCount = _context.Events.Count();;
            //var RegisteredUsers = _context.Users.Where(u=>u.)
            //var Sales = _context.Events.Count();
            //var Earnings = _context.Events.Count();
            //var eventslist = _context.Events.Where(e => e.ShowStartDate > DateTime.Now ).Take(5);


            var model = new ViewModels.AdminDashboardViewModel()
            {
                //TotalEventsCount = eventsCount,
               // UpcomingEvents = eventslist as IEnumerable<Events>,
            };
            return View(model);
        }
    }
}
