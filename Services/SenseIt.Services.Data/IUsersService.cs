using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IUsersService
    {
        Task<string> GetUserIdByReview(int reviewId);

        Task<string> GetUserIdByAppointment(int appointmentId);

        Task<bool> HasAppointments(string userId);
    }
}
