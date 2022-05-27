using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Place> Places { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedPlaces();
            modelBuilder.SeedReservations();
        }
    }
}
