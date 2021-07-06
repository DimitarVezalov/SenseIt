namespace SenseIt.Services.Data.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateProductInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(800)]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(0.1, 1000.00)]
        public decimal Price { get; set; }

        [Range(1, 1000)]
        public int InStockQuantity { get; set; }
    }
}
