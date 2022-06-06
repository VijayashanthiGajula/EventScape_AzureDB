using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventScape.Data;
using EventScape.Areas.Identity.Data;
using EventScape.Core.Repository;
using EventScape.Repositories;
using EventScape.Core;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();;

builder.Services.AddControllersWithViews();
//custom work
AddAuthorizationPolicies(builder.Services);
AddScoped();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
void AddAuthorizationPolicies(IServiceCollection services)
{
    //this is a manual work of assigning policies, which is a bad practice..
    //So to automatize this feature I have created a core folder/contants and declared constants 

    //services.AddAuthorization(options =>
    //   options.AddPolicy("RequireAdmin", policy => policy.RequireClaim("Administrator"))
    //                                 );
    //services.AddAuthorization(options =>
    //   options.AddPolicy("RequireUser", policy => policy.RequireClaim("UserRole"))
    //                                 );
    //services.AddAuthorization(options =>
    //    options.AddPolicy("RequireNonUser", policy => policy.RequireClaim("NonRegisteredUser"))
    //                                 );
    //services.AddAuthorization(options =>
    //  options.AddPolicy("RequireBlockedUser", policy => policy.RequireClaim("BlockedUser"))
    //                                 );
    builder.Services.AddAuthorization(options =>
    {

        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireUser, policy => policy.RequireRole(Constants.Roles.UserRole));
    });
}
void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRespository>();//User rep and Untof work for API purpose
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

}