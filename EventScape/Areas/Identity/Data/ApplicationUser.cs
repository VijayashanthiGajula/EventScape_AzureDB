using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventScape.Models;
using Microsoft.AspNetCore.Identity;

namespace EventScape.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }   
    public string ?LastName { get; set; }
    public byte[]? ProfilePic { get; set; }
    public ICollection<UserQueries> UserQueries { get; set; }

}

public class ApplicationRole : IdentityRole
{

}
