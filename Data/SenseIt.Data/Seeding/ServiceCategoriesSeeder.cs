namespace SenseIt.Data.Seeding
{
    using SenseIt.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ServiceCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ServiceCategories.Any())
            {
                return;
            }

            var categories = new List<string>
            {
                "Hair-Cutting, Colouring & Styling",
                "Hair Removal",
                "Hands & Feet Nail Treatments",
                "Facials & Skin Care Treatments",
                "Tanning",
                "Massages",
            };

            foreach (var category in categories)
            {
                var dbCategory = new ServiceCategory
                {
                    Name = category,
                    CreatedOn = DateTime.UtcNow,
                };

                await dbContext.ServiceCategories.AddAsync(dbCategory);
            }
        }
    }
}
