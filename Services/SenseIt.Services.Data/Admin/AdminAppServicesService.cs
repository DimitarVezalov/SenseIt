namespace SenseIt.Services.Data.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

    using SenseIt.Services.Mapping;

    using static SenseIt.Common.GlobalConstants;

    public class AdminAppServicesService : IAdminAppServicesService
    {
        private readonly IDeletableEntityRepository<Service> serviceRepository;
        private readonly IAdminServiceCategoriesService categoriesService;

        public AdminAppServicesService(IDeletableEntityRepository<Service> serviceRepository, IAdminServiceCategoriesService categoriesService)
        {
            this.serviceRepository = serviceRepository;
            this.categoriesService = categoriesService;
        }

        public async Task<int> CreateAsync(
            string name,
            string description,
            string duration,
            string category,
            string imageUrl,
            decimal price)
        {
            var categoryId = await this.categoriesService.GetCategoryIdByName(category);

            var service = new Service
            {
                Name = name,
                Description = description,
                Price = price,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                Duration = TimeSpan.Parse(duration),
            };

            await this.serviceRepository.AddAsync(service);
            await this.serviceRepository.SaveChangesAsync();

            return service.Id;
        }

        public async Task<IEnumerable<T>> GetAllAppServicesAsync<T>()
        {
            var services = await this.serviceRepository
                .AllWithDeleted()
                .To<T>()
                .ToListAsync();

            return services;
        }

        public async Task<T> GetDetailsModel<T>(int? id)
        {
            var appService = await this.serviceRepository
                .AllWithDeleted()
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return appService;
        }

        public async Task<bool> Update(
            int? id,
            string name,
            string description,
            string imageUrl,
            string category,
            string duration,
            decimal price)
        {
            if (id == null)
            {
                return false;
            }

            var categoryId = await this.categoriesService.GetCategoryIdByName(category);

            var appService = this.serviceRepository
                .AllWithDeleted()
                .FirstOrDefault(s => s.Id == id);

            appService.ImageUrl = imageUrl == null ? appService.ImageUrl : imageUrl;
            appService.Name = name;
            appService.Description = description;
            appService.Price = price;
            appService.CategoryId = categoryId;
            appService.Duration = TimeSpan.Parse(duration);

            this.serviceRepository.Update(appService);

            var result = await this.serviceRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(int? serviceId)
        {
            if (serviceId == null)
            {
                return false;
            }

            var service = await this.serviceRepository
                .All()
                .FirstOrDefaultAsync(s => s.Id == serviceId);

            this.serviceRepository.Delete(service);
            var result = await this.serviceRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Undelete(int? serviceId)
        {
            if (serviceId == null)
            {
                return false;
            }

            var service = await this.serviceRepository
                .AllWithDeleted()
                .FirstOrDefaultAsync(s => s.Id == serviceId);

            this.serviceRepository.Undelete(service);
            var result = await this.serviceRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<T> GetServiceById<T>(int? id)
        {
            var appService = await this.serviceRepository
                .AllWithDeleted()
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return appService;
        }
    }
}
