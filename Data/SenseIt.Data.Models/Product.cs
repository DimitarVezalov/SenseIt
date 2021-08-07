namespace SenseIt.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;
    using SenseIt.Data.Models.Enumerations;
    using static SenseIt.Common.GlobalConstants.ProductConstants;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.CartItems = new HashSet<CartItem>();
        }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        [ForeignKey(nameof(ProductCategory))]
        public int CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(30)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        public ProductGender Gender { get; set; }

        public decimal Price { get; set; }

        public int InStockQuantity { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
