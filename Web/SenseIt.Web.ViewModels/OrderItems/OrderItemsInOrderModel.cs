namespace SenseIt.Web.ViewModels.OrderItems
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class OrderItemsInOrderModel : IMapFrom<OrderItem>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
    }
}
