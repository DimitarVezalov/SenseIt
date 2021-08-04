namespace SenseIt.Web.ViewModels.Appointments
{
    using SenseIt.Web.ViewModels.AppServices;

    public class AppointmentIndexViewModel
    {
        public AppointmentAppServiceViewModel AppService { get; set; }

        public string Username { get; set; }

        public CreateAppointmentInputModel Appointment { get; set; }
    }
}
