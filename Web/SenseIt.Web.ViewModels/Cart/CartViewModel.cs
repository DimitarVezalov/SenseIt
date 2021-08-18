namespace SenseIt.Web.ViewModels.Cart
{
    using System.Collections.Generic;
    using System.Linq;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class CartViewModel : IMapFrom<Cart>
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public IEnumerable<CartItemViewModel> Products { get; set; }

        public decimal TotalSum => this.Products.Sum(x => x.ProductPrice * x.Quantity);
    }
}
