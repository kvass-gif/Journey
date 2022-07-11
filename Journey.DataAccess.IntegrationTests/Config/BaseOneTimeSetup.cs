using Journey.DataAccess.Database;
using Journey.DataAccess.Repositories;
using Journey.DataAccess.Services;
using Journey.DataAccess.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Journey.DataAccess.IntegrationTests.Config
{
    [SetUpFixture]
    public class BaseOneTimeSetup
    {
        protected IUnitofWork unitOfWork;
        private string[] args;

        [OneTimeSetUp]
        public void RunBeforeAnyTestsAsync()
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddIdentity();
            builder.Services.AddScoped<IClaimService, ClaimService>();
            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var identityRole = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var identityUser = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = scope.ServiceProvider.GetRequiredService<JourneyWebContext>();
            AutomatedMigration.Migrate(context);
            DatabaseContextSeed.SeedDatabase(identityRole, identityUser, context);
            unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitofWork>();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests() { }
    }
}
