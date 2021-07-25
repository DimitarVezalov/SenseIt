namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppServiceCategoriesService : IAppServiceCategoriesService
    {
        private readonly IDeletableEntityRepository<ServiceCategory> appServiceRepository;

        public AppServiceCategoriesService(IDeletableEntityRepository<ServiceCategory> appServiceRepository)
        {
            this.appServiceRepository = appServiceRepository;
        }

        public async Task<IEnumerable<T>> GetAllCategories<T>()
        {
            var categories = await this.appServiceRepository
                .All()
                .To<T>()
                .ToListAsync();

            return categories;
        }
    }
}
