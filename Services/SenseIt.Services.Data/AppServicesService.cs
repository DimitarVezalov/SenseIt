namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppServicesService : IAppServicesService
    {
        private readonly IDeletableEntityRepository<Service> appServiceRepository;

        public AppServicesService(IDeletableEntityRepository<Service> appServiceRepository)
        {
            this.appServiceRepository = appServiceRepository;
        }

        public async Task<IEnumerable<T>> GetAllByCategory<T>(int? categoryId)
        {
            var appServices = await this.appServiceRepository
                .All()
                .Where(s => s.Category.Id == categoryId)
                .To<T>()
                .ToListAsync();

            return appServices;
        }

        public async Task<T> GetAppServiceById<T>(int? id)
        {
            var appService = await this.appServiceRepository
                .AllAsNoTracking()
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return appService;
        }
    }
}
