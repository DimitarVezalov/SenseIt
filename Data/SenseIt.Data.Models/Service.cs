namespace SenseIt.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SenseIt.Data.Common.Models;

    public class Service : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"^(?:[01][0-9]|2[0-3]):[0-0][0-0]$", ErrorMessage = "Invalid time format and hh:mm values.")]
        public TimeSpan Duration { get; set; }

        [ForeignKey(nameof(ServiceCategory))]
        public int CategoryId { get; set; }

        public virtual ServiceCategory Category { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
