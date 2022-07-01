using Journey.DataAccess;
using Journey.DataAccess.Database;
using Journey.DataAccess.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
//IConfigurationRoot root = builder.Build();
//var database = root.GetSection("Database").GetSection("ConnectionString").Value;
//var contextOptions = new DbContextOptionsBuilder<JourneyWebContext>()
//    .UseSqlServer(database)
//    .Options;
//var applicationDbContext = new JourneyWebContext(contextOptions, new ClaimService());
//IUnitOfWork uof = new UnitOfWork(applicationDbContext);
//foreach (var place in uof.PlaceRepo.GetAllAsync().Result)
//{
//    Console.WriteLine(place.PlaceName);
//}

