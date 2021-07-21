namespace SenseIt.Services.Data.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data.Admin.Models.Product;

    using static SenseIt.Common.GlobalConstants;
    using static SenseIt.Common.GlobalConstants.ProductConstants;

    public class AdminProductsService : IAdminProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IAdminProductCategoriesService adminCategoriesService;

        public AdminProductsService(IDeletableEntityRepository<Product> productsRepository, IAdminProductCategoriesService adminCategoriesService)
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
                Brand = model.Brand,
                InStockQuantity = model.InStockQuantity,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product.Id;
        }

        public async Task<IEnumerable<AdminProductsListingViewModel>> GetAllProductsAsync()
        {
            var products = await this.productsRepository.AllWithDeleted()
                //.Where(x => !x.IsDeleted)
                .Include(x => x.Category)
                .Select(x => new AdminProductsListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryName = x.Category.IsDeleted ? MissingCategoryValue : x.Category.Name,
                    Price = x.Price.ToString("F2"),
                    InStockQuantity = x.InStockQuantity,
                    IsDeleted = x.IsDeleted,
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

            var dbModel = await this.productsRepository
                .AllWithDeleted()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            var model = new AdminProductDetailsViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                ImageUrl = dbModel.ImageUrl,
                Name = dbModel.Name,
                Brand = dbModel.Brand,
                Price = dbModel.Price,
                Category = dbModel.Category.IsDeleted ? MissingCategoryValue : dbModel.Category.Name,
            };

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
            product.Brand = model.Brand;
            product.CategoryId = categoryId;

            this.productsRepository.Update(product);
            var result = await this.productsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Undelete(int? productId)
        {
            if (productId == null)
            {
                return false;
            }

            var product = await this.productsRepository
                .AllWithDeleted()
                .FirstOrDefaultAsync(p => p.Id == productId);

            this.productsRepository.Undelete(product);

            var result = await this.productsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<AdminProductUpdateModel> GetProductById(int? id)
        {
            var product = await this.productsRepository
                .AllWithDeleted()
                .Where(p => p.Id == id)
                .Select(p => new AdminProductUpdateModel
                {
                    Id = p.Id,
                    Category = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                })
                .FirstOrDefaultAsync();

            return product;
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
