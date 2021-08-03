namespace SenseIt.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IDeletableEntityRepository<Appointment> appointmentRepository;

        public AppointmentsService(IDeletableEntityRepository<Appointment> appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public async Task<int> CreateUpdateAsync(
            int? appointmentId,
            string userId,
            int appServiceId,
            string startDate,
            string userFullName,
            int userAge,
            string additionalNotes)
        {

            if (appointmentId != null)
            {
                return 1;
            }
            else
            {
                var appointment = new Appointment
                {
                    UserId = userId,
                    ServiceId = appServiceId,
                    StartDate = DateTime.Parse(startDate),
                    UserFullName = userFullName,
                    UserAge = userAge,
                    AdditionalNotes = additionalNotes,
                };

                await this.appointmentRepository.AddAsync(appointment);
                var result = await this.appointmentRepository.SaveChangesAsync();
                return 2;
            }
        }
    }
}
