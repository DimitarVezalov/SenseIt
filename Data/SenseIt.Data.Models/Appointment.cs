namespace SenseIt.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;
    using SenseIt.Data.Models.Enumerations;

    public class Appointment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string CustomerFullName { get; set; }

        public int CustomerAge { get; set; }

        public string AdditionalNotes { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public DateTime StartDate { get; set; }
    }
}
