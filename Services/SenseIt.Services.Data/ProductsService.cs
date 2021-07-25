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

        public async Task<IEnumerable<T>> GetAllPaging<T>(int page, int itemsPerPage)
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

        public async Task<IEnumerable<T>> GetByCategoryPaging<T>(int page, int itemsPerPage, string category = null)
        {
            var products = await this.productsRepository
                   .AllAsNoTracking()
                   .Where(x => x.Category.Name == category)
                   .OrderBy(x => x.Id)
                   .Skip((page - 1) * itemsPerPage)
                   .Take(itemsPerPage)
                   .To<T>()
                   .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<T>> GetByGenderPaging<T>(int page, int itemsPerPage, string gender)
        {
            var products = await this.productsRepository
                   .AllAsNoTracking()
                   .Where(x => ((int)x.Gender) == ((int)Enum.Parse<ProductGender>(gender)))
                   .OrderBy(x => x.Id)
                   .Skip((page - 1) * itemsPerPage)
                   .Take(itemsPerPage)
                   .To<T>()
                   .ToListAsync();

            return products;
        }

        public int GetCount()
        {
            return this.productsRepository.All().Count();
        }

        public int GetCount(string filter)
        {
            if (this.GetGenders().Contains(filter))
            {
                return this.productsRepository
                .All()
                .Count(p => ((int)p.Gender) == ((int)Enum.Parse<ProductGender>(filter)));
            }

            return this.productsRepository
                .All()
                .Count(p => p.Category.Name == filter);
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
