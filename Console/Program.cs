using Journey.Console;
using Journey.DataAccess.Database;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
var appCofiguration = new AppConfiguration();
optionsBuilder.UseSqlServer(appCofiguration.SQLConnectionString);
using (var db = new ApplicationDbContext(optionsBuilder.Options))
{
    var el = db.Places.ToArray();
    foreach (var place in el)
    {
        Console.WriteLine(place.PlaceName);
    }
}
