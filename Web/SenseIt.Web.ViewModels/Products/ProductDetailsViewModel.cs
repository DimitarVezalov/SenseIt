namespace SenseIt.Web.ViewModels.Products
{
    using System.Collections.Generic;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    using static SenseIt.Common.GlobalConstants.ProductConstants;

    public class ProductDetailsViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

        public int InStockQuantity { get; set; }

        public string Description { get; set; }

        public bool IsAvailable => this.InStockQuantity > 10;

        public string Availability => this.IsAvailable
            ? ProductAvailableString : ProductNotAvailableString;

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ProductInListViewModel> Products { get; set; }

    }
}
