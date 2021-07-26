namespace SenseIt.Web.ViewModels.Admin.AppServices
{
    using System;
    using System.Collections.Generic;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class EditAppServiceFormModel : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
