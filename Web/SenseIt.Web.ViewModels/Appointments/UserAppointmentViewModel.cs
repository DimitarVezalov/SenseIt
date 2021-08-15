namespace SenseIt.Web.ViewModels.Appointments
{
    using System;
    using System.Globalization;
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class UserAppointmentViewModel : IMapFrom<Appointment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int CustomerAge { get; set; }

        public string CustomerFullName { get; set; }

        public string AdditionalNotes { get; set; }

        public string StartDate { get; set; }

        public string ServiceName { get; set; }

        public string ServiceImageUrl { get; set; }

        public string ServiceDuratrion { get; set; }

        public string ServiceDescription { get; set; }

        public string ServicePrice { get; set; }

        public string ServiceCategoryName { get; set; }

        public string ServiceDurationAsText => this.GetDurationText();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, UserAppointmentViewModel>()
                .ForMember(x => x.AdditionalNotes, opt =>
                opt.MapFrom(y => y.AdditionalNotes != null ? y.AdditionalNotes : "None"))
                .ForMember(x => x.StartDate, opt =>
                opt.MapFrom(y => y.StartDate.ToString("dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture)))
                .ForMember(x => x.ServiceDuratrion, opt =>
                opt.MapFrom(y => y.Service.Duration.ToString().Substring(0, 5)))
                .ForMember(x => x.ServicePrice, opt =>
                opt.MapFrom(y => y.Service.Price.ToString("F2")));
        }

        private string GetDurationText()
        {
            var hoursText = this.ServiceDuratrion[1] == '1' ? "hour" : "hours";
            var minutesText = this.ServiceDuratrion.Substring(3, 2) == "00" ? string.Empty : "minutes";
            var minutesDigits = this.ServiceDuratrion.Substring(3, 2) == "00" ? string.Empty : this.ServiceDuratrion.Substring(3, 2);
            minutesText = minutesDigits == "01" ? "minute" : "minutes";
            var minutesDigitsAndText = minutesDigits == string.Empty ? string.Empty : $"and {minutesDigits} {minutesText}";

            var durationText = $"{this.ServiceDuratrion.Substring(1, 1)} {hoursText} {minutesDigitsAndText}";

            return durationText;
        }
    }
}
