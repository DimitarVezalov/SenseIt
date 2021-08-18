using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IOrdersService
    {
        Task<int> CreateOrder(string userId, string town, string street, string number, string zipCode);

        Task<T> GetById<T>(int orderId);

        Task<IEnumerable<T>> GetAllByUser<T>(string userId);
    }
}
