namespace SenseIt.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SenseIt.Data.Models;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Products.Any())
            {
                return;
            }

            var products = new List<(string Name, string Description, string ImageUrl, decimal Price, int CategoryId)>
            {
                ("Hair Shampoo", "Very good", "https://hips.hearstapps.com/vader-prod.s3.amazonaws.com/1567015848-tresemme-1567015842.jpg?crop=1xw:1xh;center,top&resize=768:*", 20.99m, 1),
                ("Body Lotion", "Nice and Smooth", "https://images.bloomingdalesassets.com/is/image/BLM/products/4/optimized/10666204_fpx.tif?op_sharpen=1&wid=700&fit=fit,1&$filtersm$", 10.99m, 2),
                ("Nail Polish", "Beautiful Color", "https://static.beautytocare.com/media/catalog/product/cache/global/image/650x650/85e4522595efc69f496374d01ef2bf13/o/p/opi-nail-lacquer-opi-by-popular-vote-15ml.jpg", 15.99m, 3),
                ("Pedicure Nail Brush", "Cleanyyy...", "https://alexnld.com/wp-content/uploads/2019/11/TBD0173463001D.jpg", 7.99m, 4),
                ("Pedicure Nail Brush", "Cleanyyy...", "https://alexnld.com/wp-content/uploads/2019/11/TBD0173463001D.jpg", 7.99m, 4),
                ("Pedicure Nail Brush", "Cleanyyy...", "https://alexnld.com/wp-content/uploads/2019/11/TBD0173463001D.jpg", 7.99m, 4),
            };

            foreach (var product in products)
            {
                var dbProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId,
                };

                await dbContext.Products.AddAsync(dbProduct);
            }
        }
    }
}
