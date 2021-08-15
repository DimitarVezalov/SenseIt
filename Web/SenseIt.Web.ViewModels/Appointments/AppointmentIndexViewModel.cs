namespace SenseIt.Web.ViewModels.Appointments
{
    using System.Collections.Generic;

    using SenseIt.Web.ViewModels.AppServices;

    public class AppointmentIndexViewModel
    {
        public AppointmentAppServiceViewModel AppService { get; set; }

        public string Username { get; set; }

        public int LastBookedPastDays { get; set; }

        public bool HasAppointments { get; set; }

        public IEnumerable<AppointmentInModalDetailsVIewModel> ActiveAppointments { get; set; }

        public CreateAppointmentInputModel Appointment { get; set; }
    }
}
