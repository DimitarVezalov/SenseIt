namespace SenseIt.Web.ViewModels.Orders
{
    using System;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class OrderInListViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int OrderItemsCount { get; set; }
    }
}
