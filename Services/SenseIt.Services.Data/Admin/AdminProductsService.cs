namespace SenseIt.Services.Data.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data.Admin.Models;

    public class AdminProductsService : IAdminProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IAdminCategoriesService adminCategoriesService;

        public AdminProductsService(IDeletableEntityRepository<Product> productsRepository, IAdminCategoriesService adminCategoriesService)
        {
            this.productsRepository = productsRepository;
            this.adminCategoriesService = adminCategoriesService;
        }

        public async Task CreateAsync(CreateProductInputModel model)
        {
            var categoryId = this.adminCategoriesService.GetCategoryIdByName(model.Category);

            var product = new Product
            {
                Name = model.Name,
                CategoryId = categoryId,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Price = model.Price,
                InStockQuantity = model.InStockQuantity,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdminProductsListingViewModel>> GetAllProductsAsync()
        {
            var products = await this.productsRepository.All()
                .Include(x => x.Category)
                .Select(x => new AdminProductsListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryName = x.Category.Name,
                    Price = x.Price,
                    InStockQuantity = x.InStockQuantity,
                })
                .ToListAsync();

            return products;
        }
    }
}
