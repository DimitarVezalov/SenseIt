using Microsoft.EntityFrameworkCore;
using SenseIt.Data.Common.Repositories;
using SenseIt.Data.Models;
using SenseIt.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage)
        {
            var products = await this.productsRepository
                .AllAsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<T>> GetAllByCategory<T>(string categoryName, int id)
        {
            if (categoryName == null)
            {
                return null;
            }

            var products = await this.productsRepository
                .AllAsNoTracking()
                .Where(p => p.Category.Name == categoryName)
                .Where(p => p.Id != id)
                .To<T>()
                .ToListAsync();

            return products;
        }

        public int GetCount()
        {
            return this.productsRepository.All().Count();
        }

        public async Task<T> GetDetails<T>(int? id)
        {
            var product = await this.productsRepository
                .AllAsNoTracking()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return  product;
        }
    }
}
