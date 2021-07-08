namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data.Admin.Models.Categories;

    public class AdminProductCategoriesService : IAdminProductCategoriesService
    {
        private readonly IDeletableEntityRepository<ProductCategory> categoryRepository;

        public AdminProductCategoriesService(IDeletableEntityRepository<ProductCategory> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<bool> Edit(int? categoryId, ProductCategoryEditModel model)
        {
            if (categoryId == null)
            {
                return false;
            }

            var category = await this.categoryRepository
                .All()
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            category.Name = model.Name;

            this.categoryRepository.Update(category);
            var result = await this.categoryRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<ProductCategoriesListingViewModel>> GetCategoriesList()
        {
            var categories = await this.categoryRepository.All()
               .Select(c => new ProductCategoriesListingViewModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   ProductsCount = c.Products.Count,
               })
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
    }
}
