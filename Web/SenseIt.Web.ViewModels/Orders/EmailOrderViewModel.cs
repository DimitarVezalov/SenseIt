namespace SenseIt.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;
    using SenseIt.Web.ViewModels.OrderItems;

    public class EmailOrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    { 
        public int Id { get; set; }

        public string RecipientFullName { get; set; }

        public decimal TotalSum { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<OrderItemsInOrderModel> OrderItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, EmailOrderViewModel>()
                .ForMember(x => x.RecipientFullName, opt =>
                opt.MapFrom(y => $"{y.Recipient.FirstName} {y.Recipient.LastName}"));
        }
    }
}
