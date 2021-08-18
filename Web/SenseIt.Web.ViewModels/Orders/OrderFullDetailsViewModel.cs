namespace SenseIt.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;
    using SenseIt.Web.ViewModels.OrderItems;

    public class OrderFullDetailsViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string RecipientFullName { get; set; }

        public string DeliveryAddressTownName { get; set; }

        public string DeliveryAddressStreet { get; set; }

        public string DeliveryAddressNumber { get; set; }

        public string DeliveryAddressZipCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OrderStatus { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public decimal TotalSum { get; set; }

        public ICollection<OrderItemsInOrderModel> OrderItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderFullDetailsViewModel>()
                .ForMember(x => x.RecipientFullName, opt =>
                opt.MapFrom(y => $"{y.Recipient.FirstName} {y.Recipient.LastName}"))
                .ForMember(x => x.OrderStatus, opt =>
                opt.MapFrom(y => y.OrderStatus.ToString()));
        }
    }
}
