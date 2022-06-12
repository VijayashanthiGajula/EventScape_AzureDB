using System.Collections.Generic;
using EventScape.Models;

namespace EventScape.ViewModels
{
    public class AdminDashboardViewModel
    {
       public int Id { get; set; }
        public int TotalEventsCount { get; set; }
        public IEnumerable<Events>? UpcomingEvents { get; set; } 
       // public IEnumerable<UserQueries>? UserQueries { get; set; }
       // public IEnumerable<Booking>? BookingRequests { get; set; }
       // public IEnumerable<Booking>? EventSales { get; set; }
    }
}
