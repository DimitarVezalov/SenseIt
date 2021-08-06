namespace SenseIt.Services.Data
{
    using SenseIt.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<IEnumerable<T>> GetAllPaging<T>(int page, int itemsPerPage);

        Task<IEnumerable<T>> GetByGenderPaging<T>(int page, int itemsPerPage, string gender);

        Task<IEnumerable<T>> GetByCategoryPaging<T>(int page, int itemsPerPage, string category);

        Task<IEnumerable<T>> GetBySearchTermPaging<T>(int page, int itemsPerPage, string searchTerm);

        Task<IEnumerable<T>> GetAllByIds<T>(IEnumerable<ShoppingCart> cartProducts);

        int GetCount();

        Task<int> GetCount(string filter);

        Task<T> GetDetails<T>(int? id);

        Task<IEnumerable<T>> GetAllByCategory<T>(string categoryName, int id);

        IEnumerable<string> GetGenders();
    }
}
