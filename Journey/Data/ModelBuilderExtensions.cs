using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            seedPlaces(modelBuilder);
            seedReservations(modelBuilder);
        }
        private static void seedPlaces(ModelBuilder modelBuilder)
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
        private static void seedReservations(ModelBuilder modelBuilder)
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
