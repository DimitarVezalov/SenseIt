namespace SenseIt.Web.ViewModels.Admin.Categories
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class ProductCategoriesListingViewModel : IMapFrom<ProductCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int ProductsCount { get; set; }
    }
}
