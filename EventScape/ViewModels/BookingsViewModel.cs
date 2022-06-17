using EventScape.Models;

namespace EventScape.ViewModels
{
    public class BookingsViewModel
    {
        public IEnumerable<Booking>? ConfirmedBookingRequests { get; set; }
        public IEnumerable<Booking>? newBookingRequests{ get; set; }
       // public IEnumerable<Booking>? UserBookingHistorybyId { get; set; }   
    }
}
