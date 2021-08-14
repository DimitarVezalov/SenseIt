namespace SenseIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentsService
    {
        public Task<int> CreateAsync(
            string userId,
            int appServiceId,
            DateTime startDate,
            string customerFullName,
            int customerAge,
            string additionalNotes);

        Task<IEnumerable<T>> GetAllInModal<T>(string userId);

        Task<bool> CancelAppointment(int? id);

        Task<T> GetAppointmentById<T>(int? id);

        Task<IEnumerable<T>> GetAllActiveByUser<T>(string userId);

    }
}
