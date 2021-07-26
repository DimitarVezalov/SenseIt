namespace SenseIt.Web.ViewModels.Admin.AppServices
{
    using System;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AdminAppServiceListingViewModel : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsDeleted { get; set; }

        public string Category { get; set; }
    }
}
