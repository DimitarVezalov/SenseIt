namespace SenseIt.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;

    public class Appointment : BaseDeletableModel<int>
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public DateTime StartDate { get; set; }
    }
}
