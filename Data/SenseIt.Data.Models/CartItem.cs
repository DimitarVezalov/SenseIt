namespace SenseIt.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;

    public class CartItem : BaseDeletableModel<string>
    {
        public CartItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public int Quantity { get; set; }
    }
}
