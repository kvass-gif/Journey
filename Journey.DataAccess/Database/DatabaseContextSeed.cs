using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Journey.DataAccess.Database;
public static class DatabaseContextSeed
{
    public static void SeedDatabase(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager,
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
    private static void seedUsers(UserManager<IdentityUser> userManager)
    {
        if (userManager.Users.Count() < 50)
        {
            for (int i = 0; i < 50; i++)
            {
                var name = Faker.Name.First();
                if (userManager.Users.SingleOrDefault(u => u.UserName == name) == null)
                {
                    var phone = Faker.Phone.Number();
                    if (userManager.FindByNameAsync(name).Result == null)
                    {
                        IdentityUser user = new IdentityUser();
                        user.UserName = name;
                        user.Email = i < 3 ? "landlord@email.com" : "tenant@email.com";
                        user.PhoneNumber = phone;
                        IdentityResult result = userManager.CreateAsync(user, "1@Qwas").Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, i < 3 ? "LandLord" : "Tenant").Wait();
                        }
                    }
                }
            }
        }
    }
    private static void seedPlaces(JourneyWebContext context, UserManager<IdentityUser> userManager)
    {
        IList<IdentityUser> identityUsers = userManager.GetUsersInRoleAsync("LandLord").Result;
        IdentityUser identityUser = identityUsers.First();
        if (context.Places.Any() == false)
        {
            context.Places.Add(new Place()
            {
                PlaceName = "place1",
                CreatedByUserId = identityUser.Id,
                CreatedOn = DateTime.Now.Date
                
            });
            context.Places.Add(new Place()
            {
                PlaceName = "place2",
                CreatedByUserId = identityUser.Id,
                CreatedOn = DateTime.Now.Date
            });
            context.Places.Add(new Place()
            {
                PlaceName = "place3",
                CreatedByUserId = identityUser.Id,
                CreatedOn = DateTime.Now.Date
            });
        }
    }
}
