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

    using static SenseIt.Common.GlobalConstants;

    public class AdminProductsService : IAdminProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IAdminCategoriesService adminCategoriesService;

        public AdminProductsService(IDeletableEntityRepository<Product> productsRepository, IAdminCategoriesService adminCategoriesService)
        {
            this.productsRepository = productsRepository;
            this.adminCategoriesService = adminCategoriesService;
        }

        public async Task<int> CreateAsync(CreateProductInputModel model)
        {
            var categoryId = await this.adminCategoriesService.GetCategoryIdByName(model.Category);

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

            return product.Id;
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

        public async Task<bool> Delete(int? productId)
        {
            if (productId == null)
            {
                return false;
            }

            var product = await this.GetById(productId);

            this.productsRepository.Delete(product);
            var result = await this.productsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Restock(int? productId)
        {
            if (productId == null)
            {
                return false;
            }

            var product = await this.productsRepository
                .All()
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            product.InStockQuantity = ProductRestockQuantity;
            this.productsRepository.Update(product);
            var result = await this.productsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<AdminProductDetailsViewModel> GetDetailsModel(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var model = await this.productsRepository
                .All()
                .Where(p => p.Id == id)
                .Select(p => new AdminProductDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<bool> Update(int? id, AdminProductUpdateModel model)
        {
            var product = await this.GetById(id);
            var categoryId = await this.adminCategoriesService.GetCategoryIdByName(model.Category);

            product.Name = model.Name;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;
            product.CategoryId = categoryId;

            this.productsRepository.Update(product);
            var result = await this.productsRepository.SaveChangesAsync();

            return result > 0;
        }

        private async Task<Product> GetById(int? productId)
        {
            var product = await this.productsRepository
                .All()
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return product;
        }      
    }
}
