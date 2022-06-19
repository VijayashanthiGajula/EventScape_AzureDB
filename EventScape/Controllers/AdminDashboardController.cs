using EventScape.Areas.Identity.Data;
using EventScape.Core;
using EventScape.Core.Repository;
using EventScape.Data;
using EventScape.Models;
using EventScape.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace EventScape.Controllers
{
    public class AdminDashboardController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public AdminDashboardController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {          
            var Events = from s in _context.Events
                         select s;
            var eventsCount = Events.Count();
            var eventslist = Events.Where(e => e.ShowStartDate >= DateTime.Now).Take(5);
            ViewBag.NewBookingsCount = _unitOfWork.Booking.GetAll(b => b.BookingStatus == Constants.Status.BookingPending).Count();
            if (eventslist != null)
            {
                var model = new ViewModels.AdminDashboardViewModel()
                {
                    TotalEventsCount = eventsCount,
                    TotalConfirmedBookings = _unitOfWork.Booking.GetAll(b => b.BookingStatus == Constants.Status.BookingConfirmed).Count(),
                    RegisteredUsers = _unitOfWork.User.GetUsers().Count(),
                    TotalEarnings = _unitOfWork.Booking.GetAll().Sum(b => b.OrderTotal),
                    UpcomingEvents = eventslist as IEnumerable<Events>,
                    BookingRequests = _context.Booking.OrderBy(b => b.BookingDate).Take(5),
                    UserQueries=_unitOfWork.UserQueries.GetAll(q=>q.Status==Constants.Status.QueryStatusActive).Take(5),

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
