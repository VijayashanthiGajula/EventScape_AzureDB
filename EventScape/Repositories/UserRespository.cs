using EventScape.Areas.Identity.Data;
using EventScape.Core.Repository;
using EventScape.Data;

namespace EventScape.Repositories
{
    public class UserRespository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        //Get user by ID
        public ApplicationUser GetUserById(string id)
        {
 
            return _context.Users.FirstOrDefault(x => x.Id == id);
 
        }
        //ist of all users
        public ICollection<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
            _context.SaveChanges(); 
            return user;
        }
        public ApplicationUser Remove(ApplicationUser user)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
}
