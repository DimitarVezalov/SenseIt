namespace SenseIt.Services.Data.Admin.Models.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static SenseIt.Common.GlobalConstants.ProductConstants;

    public class CreateProductInputModel
    {
        [Required]
        [MinLength(ProductNameMinLength)]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ProductDescriptionMinLength)]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MinLength(ProductBrandMinLength)]
        [MaxLength(ProductBrandMaxLength)]
        public string Brand { get; set; }

        public IEnumerable<string> Categories { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Gender { get; set; }

        public IEnumerable<string> Genders { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(0.1, 1000.00)]
        public decimal Price { get; set; }

        [Range(1, 1000)]
        public int InStockQuantity { get; set; }
    }
}
