namespace SenseIt.Services.Data
{
    using SenseIt.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentsService
    {
        public Task<int> CreateAsync(
            string userId,
            int appServiceId,
            string startDate,
            string customerFullName,
            int customerAge,
            string additionalNotes);

        Task<IEnumerable<T>> GetAllByUserId<T>(string userId);

        Task<bool> CancelAppointment(int? id);

    }
}
