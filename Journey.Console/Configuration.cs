using Journey.Core.Identity;
using Journey.DataAccess;
using Journey.DataAccess.Database;
using Journey.Core.Services;
using Journey.Core.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Journey.DataAccess.Contract;

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
                .AddIdentity()
                .AddScoped<IClaimService, ClaimPrincipalService>())
                .Build();
            IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var identityRole = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var identityUser = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceScope.ServiceProvider.GetRequiredService<JourneyWebContext>();
            AutomatedMigration.Migrate(context);
            DatabaseContextSeed.SeedDatabase(identityRole, identityUser, context);
            return provider.GetRequiredService<IUnitOfWork>();
        }
    }
}
