namespace SenseIt.Services.Data
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<string> GetUserIdByReview(int reviewId);

        Task<string> GetUserIdByAppointment(int appointmentId);

        Task<string> GetUserIdByOrder(int orderId);

        Task<bool> HasAppointments(string userId);

        Task<int> SetPhoneNumber(string userId, string number);

        Task<bool> IsCartEmpty(string userId);
    }
}
