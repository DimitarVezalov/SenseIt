namespace SenseIt.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data;
    using SenseIt.Web.ViewModels.Cart;
    using SenseIt.Web.ViewModels.Products;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICartsService cartsService;
        private readonly ICartItemsService cartItemsService;

        public CartController(
            IProductsService productsService,
            ICartsService cartsService,
            ICartItemsService cartItemsService)
        {
            this.productsService = productsService;
            this.cartsService = cartsService;
            this.cartItemsService = cartItemsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int? productId, ProductDetailsViewModel input)
        {
            if (!this.ModelState.IsValid || productId == null)
            {
                return this.Error();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.cartsService.AddItemToCart(userId, productId, input.CartQuantity);

            return this.RedirectToAction("All", "Store");
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.cartsService.AddItemToCart(userId, productId, quantity);

            return this.RedirectToAction("All", "Store");
        }

        public async Task<IActionResult> Index()
        {
            var customerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var cartModel = await this.cartsService.GetCustomerCart<CartViewModel>(customerId);

            var cartItems = await this.cartItemsService.GetAllByCartId<CartItemViewModel>(cartModel.Id);
            cartModel.Products = cartItems;

            return this.View(cartModel);
        }

        public async Task<IActionResult> Remove(string id)
        {
            if (id == null)
            {
                return this.Error();
            }

            var result = await this.cartsService.RemoveItemFormCart(id);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> RemoveAll()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await this.cartsService.RemoveAll(userId);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
