using System.Threading.Tasks;

namespace SenseIt.Services.Data
{
    public interface IOrdersService
    {
        Task<int> CreateOrder();
    }
}
