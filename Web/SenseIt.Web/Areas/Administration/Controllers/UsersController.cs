namespace SenseIt.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data.Admin;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IAdminUsersService usersService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IAdminUsersService usersService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.usersService = usersService;
        }

        public async Task<IActionResult> All()
        {
            var users = await this.usersService.GetUsersAsync();

            return this.View(users);
        }

        [ActionName("AddToRole")]
        public async Task<IActionResult> AddToRoleAsync(string userId)
        {
            var roles = await this.roleManager.Roles
                .Select(r => r.Name)
                .ToListAsync();

            return this.View(roles);
        }
    }
}
