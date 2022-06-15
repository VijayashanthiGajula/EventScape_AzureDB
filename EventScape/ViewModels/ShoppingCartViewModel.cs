using EventScape.Models;

namespace EventScape.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<WishList> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
