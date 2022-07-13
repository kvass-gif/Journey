using Journey.DataAccess;
using Journey.DataAccess.Repositories;
using Journey.DataAccess.Services;
using Journey.DataAccess.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Journey.Console
{
    public static class Configuration
    {
        public static IUnitOfWork CreateUnitOfWork(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => services
                .AddDataAccess(builder.Configuration)
                .AddScoped<IClaimService, ClaimPrincipalService>())
                .Build();
            IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            return provider.GetRequiredService<IUnitOfWork>();
        }
    }
}
