namespace SenseIt.Web.ViewModels.Cart
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;
    using System;

    public class CartItemViewModel : IMapFrom<CartItem>
    {
        public string Id { get; set; }

        public int ProductId { get; set; }

        public string ProductImageUrl { get; set; }

        public string ProductName { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }
    }
}
