namespace SenseIt.Services.Data.Admin
{
    using System.Threading.Tasks;

    public interface IAdminUsersService
    {
        Task<string> CreateAsync(string username, string email, string password);
    }
}
