﻿namespace SenseIt.Services.Data.Admin.Models.AppServices
{
    using System.ComponentModel.DataAnnotations;

    public class CreateServiceInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(800)]
        public string Description { get; set; }

        [RegularExpression(@"^(?:[01][0-9]|2[0-3]):[0-0][0-0]$", ErrorMessage = "Invalid time format and hh:mm values.")]
        public string Duration { get; set; }

        [Range(0.1, 1000.00)]
        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}
