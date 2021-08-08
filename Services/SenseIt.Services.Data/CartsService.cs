namespace SenseIt.Services.Data
{
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

        public async Task AddItemToCart(string userId, int productId, int quantity)
        {
            var cart = await this.InitializeCart(userId);

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
                var created = await this.cartItemsService.Create(productId, cart.Id, quantity);
            }

        }

        public async Task<T> GetCustomerCart<T>(string customerId)
        {
            var currentCart = await this.cartRepo
                .All()
                .Include(c => c.CartItems)
                .Where(c => c.CustometId == customerId)
                .To<T>()
                .FirstOrDefaultAsync();

            return currentCart;
        }

        public async Task<int> GetCustomerCartItemsCount(string customerId)
        {
            var count = await this.cartRepo
                .All()
                .Where(c => c.CustometId == customerId)
                .Select(c => c.CartItems.Count())
                .FirstOrDefaultAsync();

            return count;

        }

        public async Task<bool> RemoveItemFormCart(string cartItemId)
        {
            var result = await this.cartItemsService.Delete(cartItemId);

            return result;
        }

        private async Task<Cart> InitializeCart(string userId)
        {
            var currentCart = this.cartRepo
                .All()
                .Include(c => c.CartItems)
                .Where(c => c.CustometId == userId)
                .FirstOrDefault();

            if (currentCart == null)
            {
                currentCart = new Cart
                {
                    CustometId = userId,
                };

                await this.cartRepo.AddAsync(currentCart);
                var result = await this.cartRepo.SaveChangesAsync();
            }

            return currentCart;
        }
    }
}
