using SenseIt.Data.Common.Repositories;
using SenseIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data.Admin
{
    public class AdminUsersService : IAdminUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public Task<string> CreateAsync(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
