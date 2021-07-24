namespace SenseIt.Services.Data
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

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage, string gender)
        {
            // TO DO: Fix the method
            if (gender == null)
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
            else
            {
                var products = await this.productsRepository
               .AllAsNoTracking()
               .Where(x => x.Gender.ToString() == gender)
               .OrderBy(x => x.Id)
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .To<T>()
               .ToListAsync();

                return products;
            }
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

        public int GetCount(string gender)
        {
            if (gender == null)
            {
                return this.GetCount();
            }

            return this.productsRepository.All().Count(x => x.Gender.ToString() == gender);
        }

        public async Task<T> GetDetails<T>(int? id)
        {
            var product = await this.productsRepository
                .AllAsNoTracking()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return product;
        }

        public IEnumerable<string> GetGenders()
        {
            var genders = Enum.GetValues(typeof(ProductGender))
                .Cast<ProductGender>()
                .Select(x => x.ToString())
                .ToList();

            return genders;
        }
    }
}
