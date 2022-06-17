using EventScape.Models;

namespace EventScape.Core.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IWishListRepository WishList { get; }
        IBookingDetailsRepository BookingDetails { get; }
        IBookingRepository Booking { get; }
        void Save();
       
    }
}
