using Microsoft.AspNetCore.Identity;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Seeding
{
    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var role = await roleManager.FindByNameAsync(GlobalConstants.UserRoleName);

            await Seed2Users(dbContext, userManager, role);
        }

        private static async Task Seed2Users(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ApplicationRole role)
        {
            var firstNames = new string[] { "Ivan", "Georgi" };

            var lastNames = new string[] { "Kaloyanov", "Dimitrov" };

            var email = string.Empty;

            var egn = string.Empty;

            const string phoneNumber = "0899123456";

            const string password = "Test_123";

            for (int i = 0; i < 2; i++)
            {
                email = $"user{i}@abv.bg";
                egn = $"054714000{i}";

                if (dbContext.Users.Any(x => x.Email == email))
                {
                    continue;
                }
                var user = new ApplicationUser()
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = email,
                    UserName = email,
                    PhoneNumber = phoneNumber,
                    EGN = egn,
                };

                if (dbContext.Users.Any(x => x.Email == email))
                {
                    continue;
                }

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, role.Name);
            }
        }
    }
}