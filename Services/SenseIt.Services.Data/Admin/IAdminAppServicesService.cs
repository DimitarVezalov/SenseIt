namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminAppServicesService
    {
        Task<int> CreateAsync(string name, string description, string duration, string category, string imageUrl, decimal price);

        Task<IEnumerable<T>> GetAllAppServicesAsync<T>();

        Task<bool> Delete(int? serviceId);

        Task<T> GetDetailsModel<T>(int? id);

        Task<bool> Update(int? id, string name, string description, string imageUrl, string category, string duration, decimal price);

        Task<T> GetServiceById<T>(int? id);

        Task<bool> Undelete(int? serviceId);
    }
}
