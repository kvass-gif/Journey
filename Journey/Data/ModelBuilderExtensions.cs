using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            seedAccounts(modelBuilder);
            seedPlaces(modelBuilder);
            seedReservations(modelBuilder);
        }
        private static void seedAccounts(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    AccountName = "Igor",
                    Password = "pass",
                    Role = Role.Tenant,
                },
                new Account
                {
                    Id = 2,
                    AccountName = "Sasha",
                    Password = "pass",
                    Role= Role.Tenant
                },
                new Account
                {
                    Id = 3,
                    AccountName = "Roma",
                    Password = "pass",
                    Role = Role.LandLord,
                }
            );
        }
        
        private static void seedPlaces(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>().HasData(
                new Place
                {
                    Id = 1,
                    PlaceName = "Apartment on Kamyanetskaya street",
                    Description = Faker.Lorem.Sentence(),
                    AccountId = 3
                },
                new Place
                {
                    Id = 2,
                    PlaceName = "Avto Spa",
                    Description = Faker.Lorem.Sentence(),
                    AccountId = 3
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
                    AccountId = 1

                },
                new Reservation
                {
                    Id = 2,
                    PlaceId = 1,
                    ArrivalDate = DateTime.Now,
                    DepartureDate = DateTime.Now,
                    IsArrived = false,
                    AccountId = 2
                },
                new Reservation
                {
                    Id = 3,
                    PlaceId = 2,
                    ArrivalDate = DateTime.Now,
                    DepartureDate = DateTime.Now,
                    IsArrived = false,
                    AccountId = 2
                }
            ); 
        }
    }
}
