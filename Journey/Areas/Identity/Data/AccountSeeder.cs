using Microsoft.AspNetCore.Identity;

namespace Journey.Areas.Identity.Data
{
    public static class AccountSeeder
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Tenant").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Tenant";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("LandLord").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "LandLord";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                for (int i = 0; i < 3; i++)
                {
                    var name = Faker.Name.First();
                    var phone = Faker.Phone.Number();
                    if (userManager.FindByNameAsync(name).Result == null)
                    {
                        IdentityUser user = new IdentityUser();
                        user.UserName = name;
                        user.Email = "landlord@email.com";
                        user.PhoneNumber = phone;
                        IdentityResult result = userManager.CreateAsync(user, "1@Qwas").Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "LandLord").Wait();
                        }
                    }
                }
                for (int i = 0; i < 50; i++)
                {
                    var name = Faker.Name.First();
                    var phone = Faker.Phone.Number();
                    if (userManager.FindByNameAsync(name).Result == null)
                    {
                        IdentityUser user = new IdentityUser();
                        user.UserName = name;
                        user.Email = "tenant@email.com";
                        user.PhoneNumber = phone;
                        IdentityResult result = userManager.CreateAsync(user, "1@Qwas").Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Tenant").Wait();
                        }
                    }
                }
            }

        }



    }
}
