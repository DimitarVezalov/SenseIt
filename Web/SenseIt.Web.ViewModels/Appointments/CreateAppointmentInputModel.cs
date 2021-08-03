namespace SenseIt.Web.ViewModels.Appointments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateAppointmentInputModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string UserFullName { get; set; }

        [Range(1, 90)]
        public int UserAge { get; set; }

        [MaxLength(400)]
        public string AdditionalNotes { get; set; }

        public int ServiceId { get; set; }

        public string StartDate { get; set; }

        public string Duration { get; set; }
    }
}
