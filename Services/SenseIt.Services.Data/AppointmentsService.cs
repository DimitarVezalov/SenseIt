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

        public async Task<bool> CancelAppointment(int? id)
        {
            if (id == null)
            {
                return false;
            }

            var appointment = this.appointmentRepository
                .All()
                .FirstOrDefault(a => a.Id == id);

            this.appointmentRepository.Delete(appointment);
            var result = await this.appointmentRepository.SaveChangesAsync();

            if (result == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<int> CreateAsync(
            string userId,
            int appServiceId,
            DateTime startDate,
            string customerFullName,
            int customerAge,
            string additionalNotes)
        {
            var appointment = new Appointment
            {
                UserId = userId,
                ServiceId = appServiceId,
                CreatedOn = DateTime.UtcNow.AddHours(3),
                StartDate = startDate,
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
