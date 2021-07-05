namespace SenseIt.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SenseIt.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string>
            {
                "Hair & Hair Style",
                "Body Care",
                "Manicure",
                "Pedicure",
            };

            foreach (var category in categories)
            {
                var dbCategory = new Category
                {
                    Name = category,
                    CreatedOn = DateTime.UtcNow,
                };

                await dbContext.Categories.AddAsync(dbCategory);
            }
        }
    }
}
