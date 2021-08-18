namespace SenseIt.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<string> GetUserIdByAppointment(int appointmentId)
        {
            var userId = await this.usersRepository
                .All()
                .Where(u => u.Appointments.Any(r => r.Id == appointmentId))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return userId;
        }

        public async Task<string> GetUserIdByOrder(int orderId)
        {
            var userId = await this.usersRepository
              .All()
              .Where(u => u.Orders.Any(r => r.Id == orderId))
              .Select(u => u.Id)
              .FirstOrDefaultAsync();

            return userId;
        }

        public async Task<string> GetUserIdByReview(int reviewId)
        {
            var userId = await this.usersRepository
                .All()
                .Where(u => u.Reviews.Any(r => r.Id == reviewId))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return userId;
        }

        public async Task<bool> HasAppointments(string userId)
        {
            var hasAppointments = await this.usersRepository
                .All()
                .Where(u => u.Id == userId)
                .Select(u => u.Appointments.Any())
                .FirstOrDefaultAsync();

            return hasAppointments;
        }

        public async Task<bool> IsCartEmpty(string userId)
        {
            var isEmpty = await this.usersRepository
                .All()
                .Include(u => u.Cart)
                .Where(u => u.Id == userId)
                .Select(u => u.Cart.CartItems.Any())
                .FirstOrDefaultAsync();

            return isEmpty;
        }

        public async Task<int> SetPhoneNumber(string userId, string number)
        {
            var user = await this.usersRepository
                .All()
                .FirstOrDefaultAsync(u => u.Id == userId);

            user.PhoneNumber = number;

            this.usersRepository.Update(user);
            var result = await this.usersRepository.SaveChangesAsync();

            return result;
        }
    }
}
