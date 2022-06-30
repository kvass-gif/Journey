using Journey.DataAccess;
using Journey.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
IConfigurationRoot root = builder.Build();
var database = root.GetSection("Database").GetSection("ConnectionString").Value;
var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseSqlServer(database)
    .Options;
ApplicationDbContext applicationDbContext = new ApplicationDbContext(contextOptions);
IUnitOfWork uof = new UnitOfWork(applicationDbContext);
foreach (var place in uof.PlaceRepo.GetAllAsync().Result)
{
    Console.WriteLine(place.PlaceName);
}

