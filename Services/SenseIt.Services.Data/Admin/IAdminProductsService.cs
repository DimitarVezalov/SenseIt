namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminProductsService
    {
        Task<int> CreateAsync(
            string category,
            string name,
            string imageUrl,
            string description,
            string brand,
            string gender,
            int quantity,
            decimal price);

        Task<IEnumerable<T>> GetAllProductsAsync<T>();

        Task<bool> Restock(int? productId);

        Task<bool> Delete(int? productId);

        Task<bool> Undelete(int? productId);

        Task<T> GetDetailsModel<T>(int? id);

        Task<T> GetProductById<T>(int? id);

        Task<bool> Update(
            int? id,
            string name,
            string description,
            string category,
            string imageUrl,
            string brand,
            string gender,
            decimal price);

        IEnumerable<string> GetGendersList();
    }
}
