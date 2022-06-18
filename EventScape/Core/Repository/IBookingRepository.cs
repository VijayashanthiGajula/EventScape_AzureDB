using EventScape.Models;
using static EventScape.Core.Repository.IRepository;

namespace EventScape.Core.Repository
{
    public interface IBookingRepository:IRepository<Booking>
    {
        void Update(Booking obj);
       
    }
}
