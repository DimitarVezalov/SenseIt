namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppServiceCategoriesService
    {
        Task<IEnumerable<T>> GetAllCategories<T>();
    }
}
