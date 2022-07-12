﻿using Journey.Application.Services;
using Journey.Application.Services.Impl;
using Journey.DataAccess.Services;
using Journey.DataAccess.Services.Impl;
using Journey.MappingProciles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Journey.Application;
public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
        return services;
    }
    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<IPlaceService, PlaceService>();
        services.AddScoped<IHomeService, HomeService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IIdentityService, IdentityService>();
    }
    
}
