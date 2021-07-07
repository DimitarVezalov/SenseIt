namespace SenseIt.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SenseIt.Data.Models;

    public class ProductCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProductCategories.Any())
            {
                return;
            }

            var categories = new List<string>
            {
                "Hair & Hair Style",
                "Face & Body Care",
                "Manicure",
                "Pedicure",
                "Face & Body Hair Removal",
            };

            foreach (var category in categories)
            {
                var dbCategory = new ProductCategory
                {
                    Name = category,
                    CreatedOn = DateTime.UtcNow,
                };

                await dbContext.ProductCategories.AddAsync(dbCategory);
            }
        }
    }
}
