namespace SenseIt.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;

    using static SenseIt.Common.GlobalConstants.ServiceConstants;

    public class Service : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ServiceNameMaxLength)]
        public string Name { get; set; }

        public TimeSpan? Duration { get; set; }

        [ForeignKey(nameof(ServiceCategory))]
        public int CategoryId { get; set; }

        public virtual ServiceCategory Category { get; set; }

        [Required]
        [MaxLength(ServiceDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
