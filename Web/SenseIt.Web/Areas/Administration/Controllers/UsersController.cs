namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Models;

    using SenseIt.Web.ViewModels.Admin.Users;

    public class UsersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> All()
        {
            var users = await this.userManager.Users
                .Select(u => new AdminUsersListingModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsLocked = u.LockoutEnd != null,
                    Roles = this.roleManager.Roles
                    .Where(r => u.Roles.Select(ur => ur.RoleId).Contains(r.Id))
                    .Select(r => r.Name)
                    .ToList(),

                })
                .ToListAsync();

            return this.View(users);
        }

        [ActionName("AddToRole")]
        public async Task<IActionResult> AddToRoleAsync()
        {
            return this.View();
        }

        [HttpPost]
        [ActionName("AddToRole")]
        public async Task<IActionResult> AddToRoleAsync(string id, AddUserToRolePostModel input)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.AddToRoleAsync)}/{id}");
            }

            var user = await this.userManager.FindByIdAsync(id);

            await this.userManager.AddToRoleAsync(user, input.Role);

            return this.RedirectToAction(nameof(this.All));
        }

        [ActionName("RemoveFromRole")]
        public async Task<IActionResult> RemoveFromRoleAsync()
        {
            return this.View();
        }

        [HttpPost]
        [ActionName("RemoveFromRole")]
        public async Task<IActionResult> RemoveFromRoleAsync(string id, RemoveUserFromRolePostModel input)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.RemoveFromRoleAsync)}/{id}");
            }

            var user = await this.userManager.FindByIdAsync(id);

            await this.userManager.RemoveFromRoleAsync(user, input.Role);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
