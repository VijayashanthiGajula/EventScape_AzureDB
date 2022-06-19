using EventScape.Core.Repository;
using EventScape.Data;
using EventScape.Models;

namespace EventScape.Repositories
{
    public class UserQueriesRepository : Repository<UserQueries>, IUserQueriesRepository
    {
        private readonly ApplicationDbContext _context;
        public UserQueriesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void append(UserQueries UserQueries, string Q1, string Q2)
        {
            UserQueries.Query = Q1+" || "+ Q2;
           
        }
        public void Save()
        {
            _context.SaveChanges();
        }
       
    }
}
