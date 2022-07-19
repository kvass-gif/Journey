using Journey.Core.Identity;
using Journey.Core.Services;
using Journey.Core.Services.Impl;
using Journey.DataAccess.Contract;
using Journey.DataAccess.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Journey.DataAccess.IntegrationTests.Config
{
    [SetUpFixture]
    public class BaseOneTimeSetup
    {
        protected IUnitOfWork unitOfWork;
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
            var identityUser = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetRequiredService<JourneyWebContext>();
            AutomatedMigration.Migrate(context);
            DatabaseContextSeed.SeedDatabase(identityRole, identityUser, context);
            unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests() { }
    }
}
