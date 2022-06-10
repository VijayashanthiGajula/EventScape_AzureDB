using EventScape.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventScape.Models;
using EventScape.ViewModels;

namespace EventScape.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    private class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           builder.Property(u=>u.FirstName).HasMaxLength(255);
            builder.Property(u=>u.LastName).HasMaxLength(255);
        }
    }

    public DbSet<EventScape.Models.Events>? Events { get; set; }
    public DbSet<EventScape.Models.UserQueries>? UserQueries { get; set; }
    public DbSet<EventScape.ViewModels.AdminDashboardViewModel>? AdminDashboardViewModel { get; set; }
}
 