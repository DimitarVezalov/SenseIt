namespace SenseIt.Services.Data.Admin.Models.Categories
{
    public class CategoriesListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int ProductsCount { get; set; }
    }
}
