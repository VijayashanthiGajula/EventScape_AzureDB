using EventScape.Areas.Identity.Data;

namespace EventScape.Core.Repository
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();
        ApplicationUser GetUserById(string id);
        ApplicationUser UpdateUser(ApplicationUser user);    

    }
}
