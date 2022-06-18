using EventScape.Models;

namespace EventScape.ViewModels
{
    public class BookingDetailsViewModel
    {
       
        public IEnumerable<BookingDetails>? BookingDetailsbyId { get; set; }
        public IEnumerable<BookingDetails>? BookingDetails { get; set; }
    }
}
