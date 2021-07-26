namespace SenseIt.Services.Data.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Data.Models.Enumerations;
    using SenseIt.Services.Mapping;
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

        public async Task<int> CreateAsync(
            string category,
            string name,
            string imageUrl,
            string description,
            string brand,
            string gender,
            int quantity,
            decimal price)
        {
            var categoryId = await this.adminCategoriesService.GetCategoryIdByName(category);

            var product = new Product
            {
                Name = name,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                Description = description,
                Price = price,
                Brand = brand,
                InStockQuantity = quantity,
                Gender = Enum.Parse<ProductGender>(gender),
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product.Id;
        }

        public async Task<IEnumerable<T>> GetAllProductsAsync<T>()
        {
            var products = await this.productsRepository
                .AllWithDeleted()
                .Include(x => x.Category)
                .To<T>()
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

        public async Task<T> GetDetailsModel<T>(int? id)
        {
            var model = await this.productsRepository
                .AllWithDeleted()
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<bool> Update(
            int? id,
            string name,
            string description,
            string category,
            string imageUrl,
            string brand,
            string gender,
            decimal price)
        {
            var product = await this.GetById(id);
            var categoryId = await this.adminCategoriesService.GetCategoryIdByName(category);

            product.Name = name;
            product.Description = description;
            product.ImageUrl = imageUrl;
            product.Price = price;
            product.Brand = brand;
            product.CategoryId = categoryId;
            product.Gender = Enum.Parse<ProductGender>(gender);

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

        public async Task<T> GetProductById<T>(int? id)
        {
            var product = await this.productsRepository
                .AllWithDeleted()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return product;
        }

        public IEnumerable<string> GetGendersList()
        {
            var genders = Enum.GetValues(typeof(ProductGender))
                .Cast<ProductGender>()
                .Select(x => x.ToString())
                .ToList();

            return genders;
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
