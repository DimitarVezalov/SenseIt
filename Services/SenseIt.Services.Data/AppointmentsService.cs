namespace SenseIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

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
                CreatedOn = DateTime.UtcNow.AddHours(3),
                StartDate = DateTime.Parse(startDate),
                CustomerFullName = customerFullName,
                CustomerAge = customerAge,
                AdditionalNotes = additionalNotes,
            };

            await this.appointmentRepository.AddAsync(appointment);
            var result = await this.appointmentRepository.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllByUserId<T>(string userId)
        {
            var appointments = await this.appointmentRepository
                .All()
                .Where(a => a.UserId == userId)
                .Where(a => a.StartDate > DateTime.UtcNow.AddHours(3))
                .To<T>()
                .ToListAsync();

            return appointments;
        }
    }
}
