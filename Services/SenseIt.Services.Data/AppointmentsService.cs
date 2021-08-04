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

        public async Task<int> CreateAsync(
            string userId,
            int appServiceId,
            string startDate,
            string customerFullName,
            int customerAge,
            string additionalNotes)
        {
            var appointment = new Appointment
            {
                UserId = userId,
                ServiceId = appServiceId,
                StartDate = DateTime.Parse(startDate),
                CustomerFullName = customerFullName,
                CustomerAge = customerAge,
                AdditionalNotes = additionalNotes,
            };

            await this.appointmentRepository.AddAsync(appointment);
            var result = await this.appointmentRepository.SaveChangesAsync();
            return result;
        }
    }
}
