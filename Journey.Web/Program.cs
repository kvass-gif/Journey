using Journey.Application;
using Journey.DataAccess;
using Journey.DataAccess.Database;

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
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        AutomatedMigration.Migrate(context);
        DatabaseContextSeed.SeedDatabase(context);
    }
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
