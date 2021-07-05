namespace SenseIt.Data.Models
{
    using SenseIt.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Product : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int InStockQuantity { get; set; }
    }
}
