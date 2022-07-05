using Journey.Application;
using Journey.DataAccess;
using Journey.DataAccess.Database;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

var app = builder.Build();
if (app.Environment.IsDevelopment() == true)
{
    app.UseExceptionHandler("/Home/Error");
    using (var scope = app.Services.CreateScope())
    {
        var identityRole = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var identityUser = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var context = scope.ServiceProvider.GetRequiredService<JourneyWebContext>();
        AutomatedMigration.Migrate(context);
        DatabaseContextSeed.SeedDatabase(identityRole, identityUser, context);
    }
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
