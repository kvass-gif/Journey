using Journey.DataAccess;
using Journey.DataAccess.Entities;
using Journey.DataAccess.Repositories;
using Journey.DataAccess.Services;
using Journey.DataAccess.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataAccess(builder.Configuration).AddIdentity();
builder.Services.AddScoped<IClaimService, ClaimService>();
var app = builder.Build();
var scope = app.Services.CreateScope();
IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
IClaimService claimService = scope.ServiceProvider.GetRequiredService<IClaimService>();
var userId = claimService.GetUserId();
Console.WriteLine(userId);
IList<Place> places = unitOfWork.PlaceRepo.GetAllAsync().Result;
foreach (var item in places)
{
    Console.WriteLine(item.PlaceName);
}
