using SenseIt.Data.Models;
using SenseIt.Services.Mapping;
using SenseIt.Web.ViewModels.OrderItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenseIt.Web.ViewModels.Orders
{
    public class OrderFullDetailsViewModel : IMapFrom<Order>
    {
        public string RecipientFullName { get; set; }

        public string AddressTownName { get; set; }

        public string AddressStreet { get; set; }

        public string AddressNumber { get; set; }

        public string AddressZipCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<OrderItemsInOrderModel> OrderItems { get; set; }
    }
}
