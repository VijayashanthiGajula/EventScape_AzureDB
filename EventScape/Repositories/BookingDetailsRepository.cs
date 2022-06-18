using EventScape.Core.Repository;
using EventScape.Data;
using EventScape.Models;

namespace EventScape.Repositories
{
    public class BookingDetailsRepository : Repository<BookingDetails>, IBookingDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingDetailsRepository(ApplicationDbContext context):base(context)
        {
            _context = context; 
        }   
        public void Update(BookingDetails obj)
        {
            _context.Update(obj);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
