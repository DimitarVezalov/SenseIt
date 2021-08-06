namespace SenseIt.Web.ViewModels.Cart
{
    using System.Collections.Generic;
    using System.Linq;

    using SenseIt.Web.ViewModels.Products;

    public class CartViewModel
    {
        public string CustomerId { get; set; }

        public IEnumerable<ProductInCartViewModel> Products { get; set; }

        public decimal TotalSum => this.Products.Sum(x => x.Price * x.Quantity);
    }
}
