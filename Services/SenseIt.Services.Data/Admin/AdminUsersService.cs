namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AdminUsersService : IAdminUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AdminUsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<T>> GetUsersAsync<T>()
        {
            var users = await this.userRepository
                .All()
                .To<T>()
                .ToListAsync();

            return users;
        }
    }
}
