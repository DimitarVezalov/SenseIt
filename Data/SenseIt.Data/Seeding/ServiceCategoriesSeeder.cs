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

            var categories = new List<(string Name, string Description, string ImageUrl)>
            {
                ("Hair-Cutting, Colouring & Styling", "From men's and women's haircuts and hair color services to conditioning treatments and special event looks, Signature Style Salons offer a full range of hair services at affordable prices. Our professional stylists will help you find the hairstyle and services that fit into your day and your life, whether it’s a quick trim or a completely new look.", "https://res.cloudinary.com/dru4l6z8l/image/upload/v1629624688/SenseIt/ServiceCategories/weqjv4u6ruc46pc15ajr.jpg"),
                ("Hair Removal", "We offer a wide range of hair removal treatments in numerous salons, to target every area of the body and face, for smooth and long-lasting results. We use the advanced PHD waxing system, the most effective and hygienic method of waxing, for consistently great results.", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629624843/SenseIt/ServiceCategories/v2g4h8u9nh8thbv7odpz.jpg"),
                ("Hands & Feet Nail Treatments", "Nail services at The Spa are offered to natural nails. Each pedicure at The Spa is enhanced with our own special protocol to offer softening, smoothing and nurturing to the skin on your hands and feet. Your nail service includes shaping the nails,  cuticle work, massage, exfoliation, base and top coats, along with two coats of your choice of color.  Following your nail professional’s home use recommendations will result in longer lasting results from your nail services.", "https://res.cloudinary.com/dru4l6z8l/image/upload/v1629625577/SenseIt/ServiceCategories/ckqzqufxjiwf4d0tiz8m.jpg"),
                ("Facials & Skin Care Treatments", "Provide facial skin care treatment. Overview. This standard is about improving and maintaining facial skin condition using a variety of treatments. These treatments include: skin exfoliation, skin warming, comedone extraction, facial massage and mask treatments.", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629625757/SenseIt/ServiceCategories/lv7amnkg3uvrz4fjeb1e.jpg"),
                ("Tanning", "Cutting-Edge UV Tanning Technology\r\nIf you're looking for the best in UV tanning, we have highly engineered tanning equipment with top-of-the-line technology. We offer level 5 booth to ensure you achieve maximum results. Our tanning booths are fast, warm, spacious and enjoyable. We also offer Sunless Spray Tanning services with Versaspa and Norvell tanning booths.\r\n \r\nComfortable and Private Spray Tanning\r\nWe offers spray tanning services with Norvell and/or Versa Pro automatic tanning booths that are entirely manufactured in the USA. These professional automatic tanning booths are cutting-edge technology for the industry in both performance, quality and class. Every tan is sprayed with full body coverage and the instructional step by step process allow for precision and detail. ", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629625821/SenseIt/ServiceCategories/xx7pgbqztuydsonrko3w.jpg"),
                ("Massages", "Massage therapy is one of the oldest healthcare practices known. References to massage are found in ancient Chinese medical texts written more than 4,000 years ago. Massage has been advocated in Western healthcare practices since the time of Hippocrates, the \"father of medicine\". Massage therapy is the scientific manipulation of the soft tissues of the body for the purpose of normalizing those tissues and consists of a group of manual techniques that include applying fixed or movable pressure, holding, and/or causing movement to parts of the body. While massage therapy is applied primarily with the hands, sometimes the forearms or elbows are used. These techniques affect the muscular, skeletal, circulatory, lymphatic, nervous, and other systems of the body.", "http://res.cloudinary.com/dru4l6z8l/image/upload/v1629625886/SenseIt/ServiceCategories/n75ntqahqfch4enbvoqw.jpg"),
            };

            foreach (var category in categories)
            {
                var dbCategory = new ServiceCategory
                {
                    Name = category.Name,
                    CreatedOn = DateTime.UtcNow,
                    Description = category.Description,
                    ImageUrl = category.ImageUrl,
                };

                await dbContext.ServiceCategories.AddAsync(dbCategory);
            }
        }
    }
}
