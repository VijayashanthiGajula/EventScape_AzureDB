using EventScape.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventScape.Core.Repository.ViewModels
{
    public class EditUserViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}

