namespace SenseIt.Services.Data
{
    using System.Threading.Tasks;

    public interface IAppointmentsService
    {
        public Task<int> CreateUpdateAsync(
            int? appointmentId,
            string userId,
            int appServiceId,
            string startDate,
            string userFullName,
            int userAge,
            string additionalNotes);
    }
}
