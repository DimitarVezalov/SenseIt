namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUsersService
    {
        Task<IEnumerable<T>> GetUsersAsync<T>();

    }
}
