using Journey.DataAccess.Database;
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(databaseConfig.ConnectionString,
                    opt => opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }
    }
}
public class DatabaseConfiguration
{
    public bool UseInMemoryDatabase { get; set; }
    public string ConnectionString { get; set; }
}
