namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class CartsService : ICartsService
    {
        private readonly IDeletableEntityRepository<Cart> cartRepo;
        private readonly IProductsService productsService;
        private readonly ICartItemsService cartItemsService;

        public CartsService(
            IDeletableEntityRepository<Cart> cartRepo,
            IProductsService productsService,
            ICartItemsService cartItemsService)
        {
            this.cartRepo = cartRepo;
            this.productsService = productsService;
            this.cartItemsService = cartItemsService;
        }

        public async Task AddItemToCart(string userId, int? productId, int quantity)
        {
            await this.InitializeCart(userId);

            var cart = await this.cartRepo
                .All()
                .Where(c => c.CustomerId == userId)
                .FirstOrDefaultAsync();

            if (cart.CartItems.Any(ci => ci.ProductId == productId))
            {
                var itemToUpdateId = cart.CartItems
                    .Where(ci => ci.ProductId == productId)
                    .Select(ci => ci.Id)
                    .FirstOrDefault();

                var updated = await this.cartItemsService.Update(itemToUpdateId, quantity);
            }
            else
            {
                cart.CartItems.Add(new CartItem { ProductId = (int)productId, Quantity = quantity });
                await this.cartRepo.SaveChangesAsync();
            }

        }

        public async Task<T> GetCustomerCart<T>(string customerId)
        {
            await this.InitializeCart(customerId);

            var currentCart = await this.cartRepo
                .All()
                .Include(c => c.CartItems)
                .Where(c => c.CustomerId == customerId)
                .To<T>()
                .FirstOrDefaultAsync();

            return currentCart;
        }

        public async Task<int> GetCustomerCartItemsCount(string customerId)
        {
            var count = await this.cartRepo
                .All()
                .Where(c => c.CustomerId == customerId)
                .Select(c => c.CartItems.Count())
                .FirstOrDefaultAsync();

            return count;

        }

        public async Task<bool> RemoveAll(string customerId)
        {
            if (customerId == null)
            {
                return false;
            }

            var cart = await this.cartRepo
                .All()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            cart.CartItems = new HashSet<CartItem>();
            this.cartRepo.Update(cart);
            var result = await this.cartRepo.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RemoveItemFormCart(string cartItemId)
        {
            var result = await this.cartItemsService.Delete(cartItemId);

            return result;
        }

        private async Task InitializeCart(string userId)
        {
            var currentCart = await this.cartRepo
                .All()
                .Include(c => c.CartItems)
                .Where(c => c.CustomerId == userId)
                .FirstOrDefaultAsync();

            if (currentCart == null)
            {
                currentCart = new Cart
                {
                    CustomerId = userId,
                };

                await this.cartRepo.AddAsync(currentCart);
                var result = await this.cartRepo.SaveChangesAsync();
            }
        }
    }
}
