using Journey.Core.Identity;
using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace Journey.DataAccess.Database;
public static class DatabaseContextSeed
{
    public static void SeedDatabase(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        JourneyWebContext context, dynamic countObjects)
    {
        seedRoles(roleManager);
        seedUsers(userManager, (int)countObjects.UsersNum);
        seedPlaces(context, userManager, (int)countObjects.PlacesNum);
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
    private static void seedUsers(UserManager<ApplicationUser> userManager, int count)
    {
        if (userManager.Users.Count() < count)
        {
            for (int i = 0; i < count; i++)
            {
                var name = Faker.Name.First();
                if (userManager.Users.SingleOrDefault(u => u.UserName == name) == null)
                {
                    var phone = Faker.Phone.Number();
                    if (userManager.FindByNameAsync(name).Result == null)
                    {
                        double countLandLords = Math.Round(((double)30 / 100) * count, MidpointRounding.AwayFromZero);
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = name;
                        user.Email = i < countLandLords ? "landlord@email.com" : "tenant@email.com";
                        user.PhoneNumber = phone;
                        IdentityResult result = userManager.CreateAsync(user, "1111").Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, i < countLandLords ? "LandLord" : "Tenant").Wait();
                        }
                    }
                }
            }
        }
    }
    private static void seedUser(UserManager<IdentityUser> userManager)
    {
        if (userManager.Users.Any() == false)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = "Igor";
            user.Email = "landlord@email.com";
            user.PhoneNumber = "+380000000000";
            IdentityResult result = userManager.CreateAsync(user, "1@Qwas").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "LandLord").Wait();
            }
        }
    }
    private static void seedPlaces(JourneyWebContext context, UserManager<ApplicationUser> userManager, int count)
    {
        var identityUsers = userManager.GetUsersInRoleAsync("LandLord").Result.ToArray();
        if (context.Places.Any() == false)
        {
            for (int i = 0; i < count; i++)
            {
                var identityUser = identityUsers[Faker.RandomNumber.Next(0, identityUsers.Length - 1)];
                context.Places.Add(new Place()
                {
                    PlaceName = Faker.Company.Name(),
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
}
