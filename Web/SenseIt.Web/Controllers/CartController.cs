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
        public async Task<IActionResult> AddToCart(int productId, ProductDetailsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
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
                return this.NotFound();
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


        // public async Task<IActionResult> Index()
        // {
        //    var shoppingCartList = new List<ShoppingCart>();

        //    if (this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart) != null
        //        && this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart).Count() > 0)
        //    {
        //        shoppingCartList = this.HttpContext.Session.Get<List<ShoppingCart>>(GlobalConstants.SessionCart);
        //    }

        //    var shoppingCartPorducts = shoppingCartList.Select(x => new ShoppingCart { ProductId = x.ProductId, Quantity = x.Quantity }).ToList();

        //    var productsInCart = await this.productsService.GetAllByIds<ProductInCartViewModel>(shoppingCartPorducts);

        //    foreach (var shopCartProd in shoppingCartPorducts)
        //    {
        //        foreach (var prod in productsInCart)
        //        {
        //            if (shopCartProd.ProductId == prod.Id)
        //            {
        //                prod.Quantity = shopCartProd.Quantity;
        //            }
        //        }
        //    }

        //    return this.View(productsInCart);
        //}

        //public IActionResult Remove(int id)
        //{
        //    var shoppingCartList = new List<ShoppingCart>();

        //    if (this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart) != null
        //        && this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart).Count() > 0)
        //    {
        //        shoppingCartList = this.HttpContext.Session.Get<List<ShoppingCart>>(GlobalConstants.SessionCart);
        //    }

        //    var prodToRemove = shoppingCartList.FirstOrDefault(p => p.ProductId == id);
        //    shoppingCartList.Remove(prodToRemove);

        //    this.HttpContext.Session.Set(GlobalConstants.SessionCart, shoppingCartList);

        //    return this.RedirectToAction(nameof(this.Index));
        //}
    }
}
