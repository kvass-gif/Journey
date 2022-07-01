using Journey.DataAccess.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journey.DataAccess;
public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddIdentity();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var json = configuration.GetSection("Database");
        var databaseConfig = new DatabaseConfiguration()
        {
            ConnectionString = json.GetSection("TestDb").Value,
            UseInMemoryDatabase = json.GetSection("UseInMemoryDatabase").Value == "true" ? true : false,
        };
        if (databaseConfig.UseInMemoryDatabase)
        {
            services.AddDbContext<JourneyWebContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
        else
        {
            services.AddDbContext<JourneyWebContext>(options =>
                options.UseSqlServer(databaseConfig.ConnectionString,
                    opt => opt.MigrationsAssembly(typeof(JourneyWebContext).Assembly.FullName)));

        }
    }
    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<JourneyWebContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });
    }
}
public class DatabaseConfiguration
{
    public bool UseInMemoryDatabase { get; set; }
    public string ConnectionString { get; set; }
}
