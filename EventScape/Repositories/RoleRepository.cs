using EventScape.Core.Repository;
using EventScape.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using EventScape.Data;

namespace EventScape.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
