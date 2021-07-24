using Microsoft.EntityFrameworkCore;
using SenseIt.Data.Common.Repositories;
using SenseIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<ProductCategory> productCategories;

        public CategoriesService(IDeletableEntityRepository<ProductCategory> productCategories)
        {
            this.productCategories = productCategories;
        }

        public async Task<IEnumerable<string>> GetProductCategories()
        {
            var categories = await this.productCategories
                .AllAsNoTracking()
                .Select(c => c.Name)
                .ToListAsync();

            return categories;
        }
    }
}
