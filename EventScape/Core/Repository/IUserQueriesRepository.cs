using EventScape.Models;
using static EventScape.Core.Repository.IRepository;

namespace EventScape.Core.Repository
{
    public interface IUserQueriesRepository : IRepository<UserQueries>
    {
        public void append(UserQueries UserQueries, string Q1, string Q2);
    }
}
