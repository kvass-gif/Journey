using Journey.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.DataAccess.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Place> Places { get; set; }
    }
}
