using EventScape.Core.Repository;
using EventScape.Data;
using Microsoft.AspNetCore.Identity;

namespace EventScape.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; }

        public IRoleRepository Role { get; }
        public IBookingDetailsRepository BookingDetails { get; }
        public IBookingRepository Booking { get; }
        public IWishListRepository WishList { get; }
        private ApplicationDbContext _context;
        
       

        public UnitOfWork(IUserRepository user, IRoleRepository role, IWishListRepository wishlist,
            IBookingRepository booking, IBookingDetailsRepository bookingdetails , ApplicationDbContext context)
        {
            User = user;
            Role = role;
            Booking = booking;
            BookingDetails = bookingdetails;
            WishList= wishlist;
            _context= context;


        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
