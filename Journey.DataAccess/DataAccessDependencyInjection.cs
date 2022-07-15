using Journey.Core.Identity;
using Journey.DataAccess.Abstraction;
using Journey.DataAccess.Database;
using Journey.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Journey.DataAccess;
public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<JourneyWebContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.Password.RequiredUniqueChars = 1;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });
        return services;
    }
    public static IServiceCollection AddCookieSettings(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        return services;
    }
    public static IServiceCollection AddAuthorizationSettings(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("TenantOnly", policy => policy
            .RequireClaim(ClaimTypes.Role, "Tenant"));
            options.AddPolicy("LandLordOnly", policy => policy
            .RequireClaim(ClaimTypes.Role, "LandLord"));
        });
        return services;
    }
    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var json = configuration.GetSection("Database");
        var databaseConfig = new DatabaseConfiguration()
        {
            ConnectionString = json.GetSection("ConnectionString").Value,
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

}
public class DatabaseConfiguration
{
    public bool UseInMemoryDatabase { get; set; }
    public string ConnectionString { get; set; }
}
