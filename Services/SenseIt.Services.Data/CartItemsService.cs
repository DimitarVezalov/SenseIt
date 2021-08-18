namespace SenseIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class CartItemsService : ICartItemsService
    {
        private readonly IDeletableEntityRepository<CartItem> cartItemRepository;

        public CartItemsService(IDeletableEntityRepository<CartItem> cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        //public async Task<int> Create(int productId, int cartId, int quantity)
        //{
        //    var cartItem = new CartItem
        //    {
        //        ProductId = productId,
        //        CartId = cartId,
        //        Quantity = quantity,

        //    };

        //    await this.cartItemRepository.AddAsync(cartItem);
        //    var result = await this.cartItemRepository.SaveChangesAsync();

        //    return result;
        //}

        public async Task<bool> Delete(string cartItemId)
        {
            var itemToRemove = await this.cartItemRepository
                .All()
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);

            this.cartItemRepository.HardDelete(itemToRemove);
            var result = await this.cartItemRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<T>> GetAllByCartId<T>(int cartId)
        {
            var cartItems = await this.cartItemRepository
                .All()
                .Where(ci => ci.CartId == cartId)
                .OrderBy(ci => ci.CreatedOn)
                .To<T>()
                .ToListAsync();

            return cartItems;
        }

        public async Task<IEnumerable<CartItem>> GetAllByUserId(string userId)
        {
            var items = await this.cartItemRepository
                .All()
                .Include(ci => ci.Product)
                .Where(ci => ci.Cart.Customer.Id == userId)
                .ToListAsync();

            return items;
        }

        public async Task<int> Update(string itemToUpdateId, int quantity)
        {
            var itemToUpdate = await this.cartItemRepository
                .All()
                .FirstOrDefaultAsync(ci => ci.Id == itemToUpdateId);

            itemToUpdate.Quantity += quantity;

            this.cartItemRepository.Update(itemToUpdate);
            var result = await this.cartItemRepository.SaveChangesAsync();

            return result;
        }
    }
}
