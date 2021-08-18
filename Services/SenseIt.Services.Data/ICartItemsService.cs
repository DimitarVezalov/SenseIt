namespace SenseIt.Services.Data
{
    using SenseIt.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartItemsService
    {
        //Task<int> Create(int productId, int cartId, int quantity);

        Task<IEnumerable<T>> GetAllByCartId<T>(int cartId);

        Task<bool> Delete(string cartItemId);

        Task<int> Update(string itemToUpdateId, int quantity);

        Task<IEnumerable<CartItem>> GetAllByUserId(string userId);
    }
}
