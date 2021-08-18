namespace SenseIt.Web.ViewModels.Orders
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;
    using System;

    public class OrderInListViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int OrderItemsCount { get; set; }
    }
}
