namespace SenseIt.Services.Data.Admin.Models.AppServices
{
    public class AdminAppServiceListingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Duration { get; set; }

        public bool IsDeleted { get; set; }

        public string Category { get; set; }
    }
}
