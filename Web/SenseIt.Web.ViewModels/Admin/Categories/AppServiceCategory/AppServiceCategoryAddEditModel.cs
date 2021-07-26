namespace SenseIt.Web.ViewModels.Admin.Categories.AppServiceCategory
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppServiceCategoryAddEditModel : IMapFrom<ServiceCategory>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
