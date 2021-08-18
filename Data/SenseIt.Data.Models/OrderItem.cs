namespace SenseIt.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;
    using static SenseIt.Common.GlobalConstants.ProductConstants;

    public class OrderItem : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
