namespace SenseIt.Data.Models
{
    using System;

    using SenseIt.Data.Common.Models;

    public class Service : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
