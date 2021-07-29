namespace SenseIt.Web.ViewModels.AppServices
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppServicesCategoriesViewModel : IMapFrom<ServiceCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
