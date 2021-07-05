namespace SenseIt.Services.Data.Admin.Models
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AdminProductsListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int InStockQuantity { get; set; }
    }
}
