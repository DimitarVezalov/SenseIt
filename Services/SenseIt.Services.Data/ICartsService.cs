namespace SenseIt.Services.Data
{
    using System.Threading.Tasks;

    public interface ICartsService
    {
        Task AddItemToCart(string userId, int productId, int quantity);

        Task<T> GetCustomerCart<T>(string customerId);

        Task<int> GetCustomerCartItemsCount(string customerId);

        Task<bool> RemoveItemFormCart(string cartItemId);
    }
}
