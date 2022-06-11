using EventScape.Areas.Identity.Data;
using EventScape.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventScape.Core.Repository.ViewModels
{
    public class EditUserViewModel
    {
        public ApplicationUser? User { get; set; }
       // public IEnumerable<IdentityRole> Roles1 { get; set; } // this is to get list of roles...
        // public IEnumerable<SelectListItem>? Roles { get; set; } used to get Ienumerable
        // datatype but in here Coz of selectLit I am using IList
        public IList<SelectListItem>? Roles { get; set; }
        

       // public IList<UserQueries>? userQueries { get; set; }
    }

    
}

