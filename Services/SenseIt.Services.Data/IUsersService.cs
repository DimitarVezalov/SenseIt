using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IUsersService
    {
        Task<string> GetUserIdByReview(int reviewId);
    }
}
