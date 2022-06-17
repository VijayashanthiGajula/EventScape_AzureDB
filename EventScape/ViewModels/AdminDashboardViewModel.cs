using EventScape.Models;

namespace EventScape.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalEventsCount { get; set;}
        public int TotalConfirmedBookings { get; set;}
        public int RegisteredUsers { get; set;}
        public decimal TotalEarnings { get; set;}
        public IEnumerable<Events>? UpcomingEvents { get; set;}
         public IEnumerable<UserQueries>? UserQueries { get; set;}
        public IEnumerable<Booking>? BookingRequests { get; set;}
         public IEnumerable<Booking>? EventSales { get; set; }

    }
}
