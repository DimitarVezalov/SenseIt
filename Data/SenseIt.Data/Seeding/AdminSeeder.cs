namespace SenseIt.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SenseIt.Data.Models;

    using static SenseIt.Common.GlobalConstants;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            await SeedAdminAsync(userManager, Admin.AdminEmail, Admin.AdminPassword, Admin.AdminUsername, Role.AdministratorRoleName);
        }

        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, string adminEmail, string adminPassword, string adminUsename, string roleName)
        {
            var user = await userManager.FindByEmailAsync(adminEmail);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = adminEmail,
                    CreatedOn = DateTime.UtcNow,
                    EmailConfirmed = true,
                    UserName = adminUsename,
                    FirstName = Admin.AdminFirstName,
                    LastName = Admin.AdminLastName,
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
