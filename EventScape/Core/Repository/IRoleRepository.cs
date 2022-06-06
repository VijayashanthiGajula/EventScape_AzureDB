using EventScape.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace EventScape.Core.Repository
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
