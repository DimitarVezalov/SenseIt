namespace SenseIt.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SenseIt.Data.Common.Models;

    using static SenseIt.Common.GlobalConstants.CategoryConstants;

    public class ProductCategory : BaseDeletableModel<int>
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}