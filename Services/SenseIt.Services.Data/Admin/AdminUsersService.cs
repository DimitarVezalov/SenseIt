namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data.Admin.Models.Users;

    public class AdminUsersService : IAdminUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AdminUsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<AdminUsersListingModel>> GetUsersAsync()
        {
            var users = await this.userRepository
                .All()
                .Select(u => new AdminUsersListingModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    IsLocked = u.LockoutEnd != null,
                })
                .ToListAsync();

            return users;
        }
    }
}
