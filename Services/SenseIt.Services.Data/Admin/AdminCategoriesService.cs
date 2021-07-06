namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

    public class AdminCategoriesService : IAdminCategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public AdminCategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
