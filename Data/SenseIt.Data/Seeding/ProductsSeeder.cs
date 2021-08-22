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

            var products = new List<(string Name, string Description, string ImageUrl, string Brand, decimal Price, int CategoryId)>
            {
                ("Hair Shampoo", "Very good", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1628764843/l2yxwkur8s5xyavcasgr.jpg", "L'oreal", 20.99m, 1),
                ("Body Lotion", "Nice and Smooth body lotion perfect for dry skin.", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629626238/SenseIt/Products/vg83rgt4xh1jndvtibpc.jpg", "Dove", 10.99m, 2),
                ("Nail Polish", "Beautiful Color", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629626513/SenseIt/Products/fuezfu5gnl9g2rlp3cro.jpg", "Opi", 15.99m, 3),
                ("Kaeso Pedicure Kit", @"Contents:
1x Citrus Squeeze Foot Soak 195ml
1x Mandarin Spritz Hygiene Spray 195ml
1x Tea Tree & Ginger Breeze Invigorating Foot Spray 195ml
1x Lime & Ginger Tingle Foot Scrub 250ml
1x Peppermint & Blueberry Twist Foot Mask 250ml
1x Mandarin & Mint Foot Yoghurt Foot & Leg Lotion 250ml", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1628762956/ynazqrdp00ydrsaz2m2r.jpg", "Kaeso", 35.00m, 4),

                ("Hair Shampoo", "Very good", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1628764843/l2yxwkur8s5xyavcasgr.jpg", "L'oreal", 20.99m, 1),
                ("Body Lotion", "Nice and Smooth body lotion perfect for dry skin.", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629626238/SenseIt/Products/vg83rgt4xh1jndvtibpc.jpg", "Dove", 10.99m, 2),
                ("Nail Polish", "Beautiful Color", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629626513/SenseIt/Products/fuezfu5gnl9g2rlp3cro.jpg", "Opi", 15.99m, 3),
                ("Kaeso Pedicure Kit", @"Contents:
1x Citrus Squeeze Foot Soak 195ml
1x Mandarin Spritz Hygiene Spray 195ml
1x Tea Tree & Ginger Breeze Invigorating Foot Spray 195ml
1x Lime & Ginger Tingle Foot Scrub 250ml
1x Peppermint & Blueberry Twist Foot Mask 250ml
1x Mandarin & Mint Foot Yoghurt Foot & Leg Lotion 250ml", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1628762956/ynazqrdp00ydrsaz2m2r.jpg", "Kaeso", 35.00m, 4),
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
                    Brand = product.Brand,
                };

                await dbContext.Products.AddAsync(dbProduct);
            }
        }
    }
}
