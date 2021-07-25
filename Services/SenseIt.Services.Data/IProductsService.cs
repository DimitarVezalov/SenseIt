namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<IEnumerable<T>> GetAllPaging<T>(int page, int itemsPerPage);

        Task<IEnumerable<T>> GetByGenderPaging<T>(int page, int itemsPerPage, string gender);

        Task<IEnumerable<T>> GetByCategoryPaging<T>(int page, int itemsPerPage, string category);

        int GetCount();

        int GetCount(string filter);

        Task<T> GetDetails<T>(int? id);

        Task<IEnumerable<T>> GetAllByCategory<T>(string categoryName, int id);

        IEnumerable<string> GetGenders();
    }
}
