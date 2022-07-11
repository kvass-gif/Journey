using Journey.Application;
using Journey.Application.Services;
using Journey.Application.Services.Impl;
using Journey.DataAccess;
using Journey.DataAccess.Services;
using Journey.DataAccess.Services.Impl;
using Journey.MappingProciles;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services
    .AddDataAccess(builder.Configuration)
    .AddScoped<IClaimService, ClaimPrincipalService>()
    .AddAutoMapper(typeof(IMappingProfilesMarker))
    .AddScoped<IHomeService, HomeService>())
    .Build();

IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

var homeService = provider.GetRequiredService<IHomeService>();
var list = homeService.GetAllByListAsync().Result;
foreach (var item in list)
{
    Console.WriteLine(item.PlaceName);
}
string jsonString = JsonSerializer.Serialize(list);
Console.WriteLine(jsonString);