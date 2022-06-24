using Microsoft.Extensions.Configuration;

namespace Journey.Console
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            var appSettings = root.GetSection("ConnectionStrings:DefaultConnection");
            SQLConnectionString = appSettings.Value;

        }
        public string SQLConnectionString { get; set; }
    }
}
