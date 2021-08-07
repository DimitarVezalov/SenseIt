namespace SenseIt.Web.ViewModels.Appointments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SenseIt.Web.Infrastructure.Attributes;

    public class CreateAppointmentInputModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string CustomerFullName { get; set; }

        [Range(1, 90)]
        public int CustomerAge { get; set; }

        [MaxLength(400)]
        public string AdditionalNotes { get; set; }

        public int ServiceId { get; set; }

        public string UserId { get; set; }

        [CustomDateTimeAttribute]
        public DateTime StartDate { get; set; }
    }
}
