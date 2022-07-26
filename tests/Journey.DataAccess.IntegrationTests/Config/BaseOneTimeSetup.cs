using Journey.Core.Identity;
using Journey.Core.Services;
using Journey.Core.Services.Impl;
using Journey.DataAccess.Database;
using Journey.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace Journey.DataAccess.IntegrationTests.Config
{
    [SetUpFixture]
    public class BaseOneTimeSetup
    {
        protected IUnitOfWork unitOfWork;
        private string[] args = new string[0];

        [OneTimeSetUp]
        public void RunBeforeAnyTestsAsync()
        {
            var builder = WebApplication.CreateBuilder(args);
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => services
                .AddDataAccess(builder.Configuration)
                .AddIdentityCore()
                .AddIdentityConfiguration()
                .AddScoped<IClaimService, ClaimPrincipalService>())
                .Build();
            IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var identityRole = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var identityUser = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceScope.ServiceProvider.GetRequiredService<JourneyWebContext>();
            AutomatedMigration.Migrate(context);
            TestSeeder.SeedDatabase(identityRole, identityUser, context);
            unitOfWork = provider.GetRequiredService<IUnitOfWork>();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests() { }
    }
}
