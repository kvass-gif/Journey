using Journey.Core.Identity;
using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Journey.DataAccess.Database
{
    public static class TestSeeder
    {
        public static void SeedDatabase(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            JourneyWebContext context)
        {
            seedRoles(roleManager);
            seedUsers(userManager);
            seedPlaces(context, userManager);
            context.SaveChanges();
        }
        private static void seedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new string[2];
            roleNames[0] = "Tenant";
            roleNames[1] = "LandLord";
            foreach (var roleName in roleNames)
            {
                if (roleManager.RoleExistsAsync(roleName).Result == false)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }
        }
        private static void seedUsers(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser();
            user.UserName = "Nayeli";
            user.Email = "landlord@email.com";// : "tenant@email.com";
            user.PhoneNumber = "+380680000000";
            var result = userManager.CreateAsync(user, "1111").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "LandLord").Wait();
            }
            user = new ApplicationUser();
            user.UserName = "Maxwell";
            user.Email = "tenant@email.com";// : "tenant@email.com";
            user.PhoneNumber = "+380680000000";
            result = userManager.CreateAsync(user, "1111").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Tenant").Wait();
            }
            user = new ApplicationUser();
            user.UserName = "William";
            user.Email = "tenant@email.com";// : "tenant@email.com";
            user.PhoneNumber = "+380680000000";
            result = userManager.CreateAsync(user, "1111").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Tenant").Wait();
            }
        }
        private static void seedPlaces(JourneyWebContext context, UserManager<ApplicationUser> userManager)
        {
            var identityUser = userManager.GetUsersInRoleAsync("LandLord").Result.First();
            context.Places.Add(new Place()
            {
                PlaceName = "Lemke, Botsford and Emmerich",
                ApplicationUserId = identityUser.Id,
                CreatedByUserId = identityUser.Id,
                CreatedOn = DateTime.Now.Date,
                Description = Faker.Lorem.Paragraph(),
                StreetAddress = Faker.Address.StreetAddress(),
                BedsCount = Faker.RandomNumber.Next(1, 4),
                Rank = Faker.RandomNumber.Next(1, 5),
                PricePerNight = Faker.RandomNumber.Next(1, 100)
            });
            context.Places.Add(new Place()
            {
                PlaceName = "Stracke, Macejkovic and Barrows",
                ApplicationUserId = identityUser.Id,
                CreatedByUserId = identityUser.Id,
                CreatedOn = DateTime.Now.Date,
                Description = Faker.Lorem.Paragraph(),
                StreetAddress = Faker.Address.StreetAddress(),
                BedsCount = Faker.RandomNumber.Next(1, 4),
                Rank = Faker.RandomNumber.Next(1, 5),
                PricePerNight = Faker.RandomNumber.Next(1, 100)
            });
            context.Places.Add(new Place()
            {
                PlaceName = "Jenkins, Rosenbaum and Schulist",
                ApplicationUserId = identityUser.Id,
                CreatedByUserId = identityUser.Id,
                CreatedOn = DateTime.Now.Date,
                Description = Faker.Lorem.Paragraph(),
                StreetAddress = Faker.Address.StreetAddress(),
                BedsCount = Faker.RandomNumber.Next(1, 4),
                Rank = Faker.RandomNumber.Next(1, 5),
                PricePerNight = Faker.RandomNumber.Next(1, 100)
            });


        }
    }
}
