namespace SenseIt.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SenseIt.Data.Common.Models;

    public class Cart : BaseDeletableModel<int>
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }

        [ForeignKey(nameof(Customer))]
        public string CustometId { get; set; }

        public ApplicationUser Customer { get; set; }

        public decimal TotalSum => this.CartItems.Sum(ci => ci.Product.Price * ci.Quantity);

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}