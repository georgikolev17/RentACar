using Microsoft.AspNetCore.Identity;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Seeding
{
    public class AdministratorSeeder : ISeeder
    {
        private const string AdminEmail = "admin@admin.com";
        private const string AdminPassword = "Test_123";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var role = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);

            if (role == null)
            {
                await new RolesSeeder().SeedAsync(dbContext, serviceProvider);
            }

            if (dbContext.Users.Any(x => x.Email == AdminEmail))
            {
                return;
            }

            var user = new ApplicationUser()
            {
                Email = AdminEmail,
                UserName = AdminEmail,
                FirstName = "Admin",
                LastName = "Adminov",
                EGN = "0123456788",
                PhoneNumber = "0895506983",
            };

            var result = await userManager.CreateAsync(user, AdminPassword);

            await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}