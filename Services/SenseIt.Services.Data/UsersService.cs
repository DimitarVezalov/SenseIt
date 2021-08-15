using Microsoft.EntityFrameworkCore;
using SenseIt.Data.Common.Repositories;
using SenseIt.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
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

        public async Task<string> GetUserIdByReview(int reviewId)
        {
            var userId = await this.usersRepository
                .All()
                .Where(u => u.Reviews.Any(r => r.Id == reviewId))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return userId;
        }
    }
}
