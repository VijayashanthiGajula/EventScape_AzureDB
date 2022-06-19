using EventScape.Areas.Identity.Data;
using EventScape.Models;

namespace EventScape.ViewModels
{
    public class UserQueriesViewModel
    {
        public IEnumerable<UserQueries>? Queries { get; set; }
        //public IEnumerable<UserQueries>? RepliedQueries{ get; set; }
        public ApplicationUser User { get; set; }
        public Events Event { get; set; }
    }
}
