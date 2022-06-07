﻿using Journey.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data
{
    public static class ApplicationDbSeeder
    {
        private static IdentityUser[] findUsersByRole(AccountDbContext accountDbContext, string role)
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
        private static void seedFacilitiesPlaces(DbSet<Place> places, 
            Facility[] facilities, DbSet<FacilityPlace> facilityPlaces)
        {
            if (!facilityPlaces.Any())
            {
                foreach (var place in places)
                {
                    foreach (var facility in facilities)
                    {
                        if(Faker.Boolean.Random())
                        {
                            facilityPlaces.Add(new FacilityPlace()
                            {
                                PlaceId = place.Id,
                                FacilityId = facility.Id
                            });
                        }
                    }
                }
            }
        }
        private static void seedFacilities(DbSet<Facility> facilities)
        {
            if (!facilities.Any())
            {
                facilities.Add(new Facility() { Name = "Internet" });
                facilities.Add(new Facility() { Name = "Gym" });
                facilities.Add(new Facility() { Name = "Tv" });
                facilities.Add(new Facility() { Name = "Kitchen" });
            }
        }
        private static void seedTypes(DbSet<PlaceType> types)
        {
            if (!types.Any())
            {
                types.Add(new PlaceType() { TypeName = "An entire place" });
                types.Add(new PlaceType() { TypeName = "A private room" });
                types.Add(new PlaceType() { TypeName = "A shared room" });
            }
        }

        private static void seedCities(DbSet<City> cities)
        {
            if (!cities.Any())
            {
                cities.Add(new City() { CityName = "Tokyo" });
                cities.Add(new City() { CityName = "Delhi" });
                cities.Add(new City() { CityName = "Shanghai" });
                cities.Add(new City() { CityName = "Sao Paulo" });
                cities.Add(new City() { CityName = "Mexico City" });
            }
        }
        private static void seedPlaces(DbSet<Place> places, IdentityUser[] landLordUsers)
        {
            if (!places.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    var place = new Place()
                    {
                        PlaceName = Faker.Company.Name(),
                        Description = Faker.Lorem.Sentence(),
                        Rank = Faker.RandomNumber.Next(1, 5),
                        PricePerNight = Faker.RandomNumber.Next(1, 100),
                        Address = Faker.Address.StreetAddress(),
                        CityId = Faker.RandomNumber.Next(1, 5),
                        PlaceTypeId = Faker.RandomNumber.Next(1, 3),
                        AccountId = landLordUsers[Faker.RandomNumber.Next(0, landLordUsers.Length - 1)].Id,
                        CreatedAt = DateTime.Now.Date
                        .AddMonths(-1 * Faker.RandomNumber.Next(0, 12))
                        .AddDays(-1 * Faker.RandomNumber.Next(0, 30))
                        .AddYears(-1 * Faker.RandomNumber.Next(0, 1)),
                    };
                    if (place.PlaceTypeId == 3)
                    {
                        place.BedsCount = 1;
                    }
                    else
                    {
                        place.BedsCount = Faker.RandomNumber.Next(1, 5);
                    }
                    places.Add(place);
                }
            }
        }
        private static void seedReservations(DbSet<Reservation> reservations,
            DbSet<Place> places, IdentityUser[] tenantUsers)
        {
            if (!reservations.Any())
            {
                foreach (var place in places)
                {
                    var startDate = DateTime.Now.Date.AddMonths(-12);
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
        public static void SeedData(ApplicationDbContext applicationDbContext, AccountDbContext accountDbContext)
        {
            seedCities(applicationDbContext.Cities);
            applicationDbContext.SaveChanges();
            seedTypes(applicationDbContext.PlaceTypes);
            applicationDbContext.SaveChanges();
            seedFacilities(applicationDbContext.Facilities);
            applicationDbContext.SaveChanges();
            var landLords = findUsersByRole(accountDbContext, "LandLord");
            seedPlaces(applicationDbContext.Places, landLords);
            applicationDbContext.SaveChanges();
            seedFacilitiesPlaces(applicationDbContext.Places, applicationDbContext.Facilities.ToArray(),
                applicationDbContext.FacilityPlaces);
            applicationDbContext.SaveChanges();
            var tenants = findUsersByRole(accountDbContext, "Tenant");
            seedReservations(applicationDbContext.Reservations, applicationDbContext.Places, tenants);
            applicationDbContext.SaveChanges();
        }
    }
}
