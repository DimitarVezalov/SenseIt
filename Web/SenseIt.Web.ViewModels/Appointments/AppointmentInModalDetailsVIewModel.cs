namespace SenseIt.Web.ViewModels.Appointments
{
    using System;
    using System.Globalization;

    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppointmentInModalDetailsVIewModel : IMapFrom<Appointment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ServiceId { get; set; }

        public string CustomerFullName { get; set; }

        public int CustomerAge { get; set; }

        public string StartDate { get; set; }

        public string AdditionalNotes { get; set; }

        public string Status { get; set; }

        public string ServiceImageUrl { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrice { get; set; }

        public string ServiceCategoryName { get; set; }

        public TimeSpan ServiceDuration { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, AppointmentInModalDetailsVIewModel>()
                .ForMember(x => x.StartDate, opt =>
                opt.MapFrom(y => y.StartDate.ToString("dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Status, opt =>
                opt.MapFrom(y => y.StartDate > DateTime.UtcNow ? "Active" : "Not Active"));
        }
    }
}
