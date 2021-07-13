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
    using SenseIt.Services.Data.Admin.Models.AppServices;

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

        public async Task<int> CreateAsync(CreateAppServiceInputModel model)
        {
            var categoryId = await this.categoriesService.GetCategoryIdByName(model.Category);

            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = categoryId,
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.UtcNow,
                Duration = TimeSpan.Parse(model.Duration),
            };

            await this.serviceRepository.AddAsync(service);
            await this.serviceRepository.SaveChangesAsync();

            return service.Id;
        }

        public async Task<IEnumerable<AdminAppServiceListingViewModel>> GetAllServicesAsync()
        {
            var services = await this.serviceRepository
                .AllWithDeleted()
                .Select(x => new AdminAppServiceListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Duration = x.Duration.ToString().Substring(0, 5),
                    Category = x.Category.IsDeleted ? MissingCategoryValue : x.Category.Name,
                    Price = x.Price.ToString(),
                    IsDeleted = x.IsDeleted,
                })
                .ToListAsync();

            return services;
        }

        public async Task<AppServiceDetailsModel> GetDetailsModel(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var appService = await this.serviceRepository
                .AllWithDeleted()
                .Where(s => s.Id == id)
                .Select(s => new AppServiceDetailsModel
                {
                    Id = s.Id,
                    Category = s.Category.Name,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    Name = s.Name,
                    Price = s.Price,
                    Duration = s.Duration.ToString().Substring(0, 5),
                })
                .FirstOrDefaultAsync();

            return appService;
        }

        public async Task<bool> Update(int? id, EditAppServiceInputModel model)
        {
            if (id == null)
            {
                return false;
            }

            var categoryId = await this.categoriesService.GetCategoryIdByName(model.Category);

            var appService = this.serviceRepository
                .AllWithDeleted()
                .FirstOrDefault(s => s.Id == id);

            appService.ImageUrl = model.ImageUrl;
            appService.Name = model.Name;
            appService.Description = model.Description;
            appService.Price = model.Price;
            appService.CategoryId = categoryId;
            appService.Duration = TimeSpan.Parse(model.Duration);

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
    }
}
