using EventScape.Models;
using static EventScape.Core.Repository.IRepository;

namespace EventScape.Core.Repository
{
    public interface IWishListRepository:IRepository<WishList>
    {
        //WishList Update(WishList obj);
       int IncrementTickets(WishList wishList, int count);
        int DecrementTickets(WishList wishList, int count);
    }
}
