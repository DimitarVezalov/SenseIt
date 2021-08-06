namespace SenseIt.Web.ViewModels.Products
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class ProductInCartViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
