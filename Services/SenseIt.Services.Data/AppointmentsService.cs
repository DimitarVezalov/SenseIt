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

            this.appointmentRepository.HardDelete(appointment);
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
                CreatedOn = DateTime.UtcNow,
                StartDate = startDate,
                CustomerFullName = customerFullName,
                CustomerAge = customerAge,
                AdditionalNotes = additionalNotes,
            };

            await this.appointmentRepository.AddAsync(appointment);
            var result = await this.appointmentRepository.SaveChangesAsync();
            return appointment.Id;
        }

        public async Task<IEnumerable<T>> GetAllActiveByUser<T>(string userId)
        {
            var appointments = await this.appointmentRepository
                .AllAsNoTracking()
                .Where(a => a.UserId == userId)
                .Where(a => a.StartDate > DateTime.UtcNow)
                .To<T>()
                .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<T>> GetAllInModal<T>(string userId)
        {
            var appointments = await this.appointmentRepository
                .All()
                .Where(a => a.UserId == userId)
                .Where(a => a.StartDate > DateTime.UtcNow)
                .To<T>()
                .ToListAsync();

            return appointments;
        }

        public async Task<T> GetAppointmentById<T>(int? id)
        {
            var appointment = await this.appointmentRepository
                .All()
                .Where(a => a.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return appointment;
        }
    }
}
