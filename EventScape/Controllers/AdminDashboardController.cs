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
           
            // var RegisteredUsers = _context.Users.Where(u=>u.)
            //var Sales = _context.Events.Count();
            //var Earnings = _context.Events.Count();
            var Events = from s in _context.Events
                         select s;
            var eventsCount = Events.Count();
            var eventslist = Events.Where(e => e.ShowStartDate >= DateTime.Now).Take(5);

            if (eventslist != null)
            {
                var model = new ViewModels.AdminDashboardViewModel()
                {
                    TotalEventsCount = eventsCount,
                    UpcomingEvents = eventslist as IEnumerable<Events>,
                };
                return View(model);
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
           

                    
                    
        }
    }
}
