namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppServicesService
    {
        Task<IEnumerable<T>> GetAllByCategory<T>(int? id);

        Task<T> GetAppServiceById<T>(int? id);
    }
}
