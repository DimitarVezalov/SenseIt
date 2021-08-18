namespace SenseIt.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;
    using SenseIt.Web.ViewModels.OrderItems;

    public class OrderDetailsViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public decimal TotalSum { get; set; }

        public ICollection<OrderItemsInOrderModel> OrderItems { get; set; }
    }
}
