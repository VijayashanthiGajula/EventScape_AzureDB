using EventScape.Core.Repository;
using EventScape.Data;
using EventScape.Models;

namespace EventScape.Repositories
{
    public class WishListRepository : Repository<WishList>, IWishListRepository
    {
        private readonly ApplicationDbContext _context;
        public WishListRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int DecrementTickets(WishList wishList, int count)
        {
            wishList.Tickets -= count;
            return wishList.Tickets;
        }

        public int IncrementTickets(WishList wishList, int count)
        {

            wishList.Tickets += count;
            return wishList.Tickets;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        //public WishList Update(WishList obj)
        //{
        //    _context.Update(obj);
        //    _context.SaveChanges();
        //    return obj;
        //}
    }
}
