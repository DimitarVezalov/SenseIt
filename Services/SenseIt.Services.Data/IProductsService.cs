namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage = 9);

        int GetCount();
    }
}
