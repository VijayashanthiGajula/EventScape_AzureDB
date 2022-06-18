using EventScape.Core.Repository;
using EventScape.Data;
using EventScape.Models;

namespace EventScape.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context):base(context)
        {
            _context = context; 
        }   
        public void Update(Booking obj)
        {
            _context.Update(obj);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
