namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

    public class ProductCategoriesService : IProductCategoriesService
    {
        private readonly IDeletableEntityRepository<ProductCategory> productCategories;

        public ProductCategoriesService(IDeletableEntityRepository<ProductCategory> productCategories)
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
