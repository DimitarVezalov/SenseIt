namespace SenseIt.Web.ViewModels.Appointments
{
    using System;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class UserAppointmentsListViewModel : IMapFrom<Appointment>
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public string ServiceName { get; set; }

        public string Status => this.StartDate > DateTime.UtcNow ? "Active" : "Not Active";
    }
}
