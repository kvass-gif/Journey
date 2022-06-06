using Journey.Areas.Identity.Data;
using Journey.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AccountConnection") ?? throw new InvalidOperationException("Connection string 'AccountDbContextConnection' not found.");
builder.Services.AddDbContext<AccountDbContext>(options =>
    options.UseSqlServer(connectionString)); ;
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AccountDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TenantOnly", policy => policy
    .RequireClaim(ClaimTypes.Role, "Tenant"));
    options.AddPolicy("LandLordOnly", policy => policy
    .RequireClaim(ClaimTypes.Role, "LandLord"));
});
builder.Services.AddScoped<AccountUnitOfWork>();
///////////////////////////////////////////////////////////////////////////////////
string connection = builder.Configuration.GetConnectionString("ApplicationConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connection);
});
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddControllersWithViews();
var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var accountDbContext = services.GetRequiredService<AccountDbContext>();
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    AccountSeeder.SeedData(userManager, roleManager);
    ApplicationDbSeeder.SeedData(dbContext, accountDbContext);
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=LandLord}/{action=Reservations}/{PlaceId=301}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
