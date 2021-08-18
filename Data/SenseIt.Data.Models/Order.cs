namespace SenseIt.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;
    using SenseIt.Data.Models.Enumerations;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        [ForeignKey(nameof(Recipient))]
        public string RecipientId { get; set; }

        public ApplicationUser Recipient { get; set; }

        [ForeignKey(nameof(DeliveryAddress))]
        public int DeliveryAddressId { get; set; }

        public Address DeliveryAddress { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public decimal TotalSum { get; set; }

    }
}
