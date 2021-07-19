namespace SenseIt.Services.Data.Admin.Models.AppServices
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static SenseIt.Common.GlobalConstants.ServiceConstants;

    public class CreateAppServiceInputModel
    {
        [Required]
        [MinLength(ServiceNameMinLength)]
        [MaxLength(ServiceNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ServiceDescriptionMinLength)]
        [MaxLength(ServiceDescriptionMaxLength)]
        public string Description { get; set; }

        [RegularExpression(ServiceDurationRegex, ErrorMessage = ServiceDurationExMessagge)]
        public string Duration { get; set; }

        [Required]
        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(0.1, 1000.00)]
        public decimal Price { get; set; }

    }
}
