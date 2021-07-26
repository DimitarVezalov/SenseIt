namespace SenseIt.Services.Data.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AdminProductCategoriesService : IAdminProductCategoriesService
    {
        private readonly IDeletableEntityRepository<ProductCategory> categoryRepository;

        public AdminProductCategoriesService(IDeletableEntityRepository<ProductCategory> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new ProductCategory
            {
                Name = name,
            };

            var result = this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> Edit(int? categoryId, string name)
        {
            if (categoryId == null)
            {
                return false;
            }

            var category = await this.categoryRepository
                .All()
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            category.Name = name;

            this.categoryRepository.Update(category);
            var result = await this.categoryRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<T>> GetCategoriesList<T>()
        {
            var categories = await this.categoryRepository
                .AllWithDeleted()
                .To<T>()
                .ToListAsync();

            return categories;
        }

        public async Task<int> GetCategoryIdByName(string name)
        {
            var categoryId = await this.categoryRepository.All()
                .Where(c => c.Name == name)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            return categoryId;
        }

        public async Task<IEnumerable<string>> GetCategoryNames()
        {
            var categories = await this.categoryRepository.All()
                .Select(c => c.Name)
                .ToListAsync();

            return categories;
        }

        public async Task<bool> Delete(int? categoryId)
        {
            if (categoryId == null)
            {
                return false;
            }

            var category = await this.categoryRepository
                .All()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            this.categoryRepository.Delete(category);

            var result = await this.categoryRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Undelete(int? categoryId)
        {
            if (categoryId == null)
            {
                return false;
            }

            var category = await this.categoryRepository
                .AllWithDeleted()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            this.categoryRepository.Undelete(category);

            var result = await this.categoryRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<T> GetEditModel<T>(int? id)
        {
            var model = await this.categoryRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return model;
        }
    }
}
