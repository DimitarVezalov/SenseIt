using AutoMapper;
using SenseIt.Data.Models;
using SenseIt.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenseIt.Web.ViewModels.Appointments
{
    public class EmailAppointmentViewModel : IMapFrom<Appointment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CustomerFullName { get; set; }

        public string ServiceName { get; set; }

        public string ServiceDuration { get; set; }

        public string StartDate { get; set; }

        public string ServiceImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Appointment, EmailAppointmentViewModel>()
                .ForMember(x => x.ServiceDuration, opt =>
                opt.MapFrom(y => y.Service.Duration.ToString().Substring(0, 5)))
                .ForMember(x => x.StartDate, opt =>
                opt.MapFrom(y => y.StartDate.ToString().Substring(0, 16)));
        }
    }
}
