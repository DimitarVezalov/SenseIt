using Microsoft.EntityFrameworkCore;
using SenseIt.Data.Common.Repositories;
using SenseIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data.Admin
{
    public class AdminCategoriesService : IAdminCategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public AdminCategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public int GetCategoryIdByName(string name)
        {
            var categoryId = this.categoryRepository.All()
                .Where(c => c.Name == name)
                .Select(c => c.Id)
                .FirstOrDefault();

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
