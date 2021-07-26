namespace SenseIt.Web.ViewModels.Admin.Categories.AppServiceCategory
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppServiceCategoriesListingModel : IMapFrom<ServiceCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int ServicesCount { get; set; }
    }
}
