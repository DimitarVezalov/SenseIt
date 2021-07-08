namespace SenseIt.Services.Data.Admin
{
    using System;
    using System.Collections.Generic;
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

        public async Task<int> CreateAsync(CreateServiceInputModel model)
        {
            var categoryId = await this.categoriesService.GetCategoryIdByName(model.Category);

            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = categoryId,
                CreatedOn = DateTime.UtcNow,
                Duration = TimeSpan.Parse(model.Duration),
            };

            await this.serviceRepository.AddAsync(service);
            await this.serviceRepository.SaveChangesAsync();

            return service.Id;
        }

        public async Task<IEnumerable<AdminServiceListingViewModel>> GetAllServicesAsync()
        {
            var services = await this.serviceRepository
                .AllWithDeleted()
                .Where(x => !x.IsDeleted)
                .Select(x => new AdminServiceListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Duration = x.Duration.ToString(),
                    Category = x.Category.IsDeleted ? MissingCategoryValue : x.Category.Name,
                    Price = x.Price.ToString(),
                })
                .ToListAsync();

            return services;
        }
    }
}
