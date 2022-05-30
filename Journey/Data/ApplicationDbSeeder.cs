using Journey.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data
{
    public static class ApplicationDbSeeder
    {
        private static IdentityUser[] FindUsersByRole(AccountDbContext accountDbContext, string role)
        {
            var users = (from r in accountDbContext.Roles
                         where r.Name == role
                         from ur in accountDbContext.UserRoles
                         where ur.RoleId == r.Id
                         from u in accountDbContext.Users
                         where u.Id == ur.UserId
                         select u).ToArray();
            return users;
        }
        public static void SeedData(ApplicationDbContext applicationDbContext, AccountDbContext accountDbContext)
        {
            SeedCities(applicationDbContext.Cities);
            applicationDbContext.SaveChanges();
            var landLords = FindUsersByRole(accountDbContext, "LandLord");
            SeedPlaces(applicationDbContext.Places, landLords);
            applicationDbContext.SaveChanges();
            var tenants = FindUsersByRole(accountDbContext, "Tenant");
            SeedReservations(applicationDbContext.Reservations, applicationDbContext.Places, tenants);
            applicationDbContext.SaveChanges();
        }

        private static void SeedCities(DbSet<City> cities)
        {
            if (!cities.Any())
            {
                cities.Add(new City(){CityName = "Tokyo"});
                cities.Add(new City(){CityName = "Delhi" });
                cities.Add(new City(){CityName = "Shanghai" });
                cities.Add(new City(){CityName = "Sao Paulo" });
                cities.Add(new City(){CityName = "Mexico City" });
            }
        }

        public static void SeedPlaces(DbSet<Place> places, IdentityUser[] landLordUsers)
        {
            if (!places.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    places.Add(new Place()
                    {
                        PlaceName = Faker.Company.Name(),
                        Description = Faker.Lorem.Sentence(),
                        Rank = Faker.RandomNumber.Next(1, 5),
                        PricePerNight = Faker.RandomNumber.Next(1, 100),
                        Address = Faker.Address.StreetAddress(),
                        CityId = Faker.RandomNumber.Next(1, 5),
                        AccountId = landLordUsers[Faker.RandomNumber.Next(0, landLordUsers.Length - 1)].Id
                    });
                }
            }
        }
        public static void SeedReservations(DbSet<Reservation> reservations,
            DbSet<Place> places, IdentityUser[] tenantUsers)
        {
            if (!reservations.Any())
            {
                foreach (var place in places)
                {
                    var startDate = DateTime.Now.AddMonths(-12);
                    var endDate = startDate;
                    for (int i = 0; i < 100; i++)
                    {
                        startDate = endDate.AddDays(Faker.RandomNumber.Next(0, 15));
                        endDate = startDate.AddDays(Faker.RandomNumber.Next(1, 15));
                        Status status;
                        bool isPaid;
                        if (DateTime.Now > endDate)
                        {
                            isPaid = Faker.Boolean.Random();
                            if (isPaid)
                            {
                                status = Status.Completed;
                            }
                            else
                            {
                                status = Status.Canceled;
                            }
                        }
                        else
                        {
                            isPaid = true;
                            if (Faker.Boolean.Random())
                            {
                                status = Status.Waiting;
                            }
                            else
                            {
                                status = Status.Completed;
                            }
                        }
                        reservations.Add(
                            new Reservation()
                            {
                                PlaceId = place.Id,
                                ArrivalDate = startDate,
                                DepartureDate = endDate,
                                IsPaid = isPaid,
                                Status = status,
                                AccountId = tenantUsers[Faker.RandomNumber.Next(0, tenantUsers.Length - 1)].Id
                            });
                    }
                }

            }
        }
    }
}
