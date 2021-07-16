namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SenseIt.Services.Data.Admin.Models.Users;

    public interface IAdminUsersService
    {
        Task<IEnumerable<AdminUsersListingModel>> GetUsersAsync();

    }
}
