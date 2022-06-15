using EventScape.Models;
using static EventScape.Core.Repository.IRepository;

namespace EventScape.Core.Repository
{
    public interface IBookingDetailsRepository : IRepository<BookingDetails>
    {
        void Update(BookingDetails obj);
      
    }
}
