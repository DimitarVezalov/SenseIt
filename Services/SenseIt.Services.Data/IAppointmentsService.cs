namespace SenseIt.Services.Data
{
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
    }
}
