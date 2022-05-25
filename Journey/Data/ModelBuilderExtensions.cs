using Journey.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tenant",
                    NormalizedName = "TENANT"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "LandLord",
                    NormalizedName = "LANDLORD"
                }
            );
        }
        public static void SeedPlaces(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>().HasData(
                new Place
                {
                    Id = 1,
                    PlaceName = "Apartment on Kamyanetskaya street",
                    Description = Faker.Lorem.Sentence(),
                },
                new Place
                {
                    Id = 2,
                    PlaceName = "Avto Spa",
                    Description = Faker.Lorem.Sentence(),
                }
            );
        }
        public static void SeedReservations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    PlaceId = 1,
                    ArrivalDate = DateTime.Now,
                    DepartureDate = DateTime.Now,
                    IsArrived = false,

                },
                new Reservation
                {
                    Id = 2,
                    PlaceId = 1,
                    ArrivalDate = DateTime.Now,
                    DepartureDate = DateTime.Now,
                    IsArrived = false,
                },
                new Reservation
                {
                    Id = 3,
                    PlaceId = 2,
                    ArrivalDate = DateTime.Now,
                    DepartureDate = DateTime.Now,
                    IsArrived = false,
                }
            );
        }
    }
}
