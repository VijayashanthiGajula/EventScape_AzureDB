using EventScape.Models;

namespace EventScape.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<WishList> CartItems { get; set; }
       // public decimal CartTotal { get; set; }
        public Booking  Booking { get; set; }   

    }
}
