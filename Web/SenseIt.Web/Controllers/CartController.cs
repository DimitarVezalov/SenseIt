namespace SenseIt.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Common;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data;
    using SenseIt.Web.Utility;
    using SenseIt.Web.ViewModels.Products;


    [Authorize]
    public class CartController : BaseController
    {
        private readonly IProductsService productsService;

        public CartController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var shoppingCartList = new List<ShoppingCart>();

            if (this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart) != null
                && this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = this.HttpContext.Session.Get<List<ShoppingCart>>(GlobalConstants.SessionCart);
            }

            var shoppingCartPorducts = shoppingCartList.Select(x => new ShoppingCart { ProductId = x.ProductId, Quantity = x.Quantity }).ToList();

            var productsInCart = await this.productsService.GetAllByIds<ProductInCartViewModel>(shoppingCartPorducts);

            foreach (var shopCartProd in shoppingCartPorducts)
            {
                foreach (var prod in productsInCart)
                {
                    if (shopCartProd.ProductId == prod.Id)
                    {
                        prod.Quantity = shopCartProd.Quantity;
                    }
                }
            }

            return this.View(productsInCart);
        }

        public IActionResult Remove(int id)
        {
            var shoppingCartList = new List<ShoppingCart>();

            if (this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart) != null
                && this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = this.HttpContext.Session.Get<List<ShoppingCart>>(GlobalConstants.SessionCart);
            }

            var prodToRemove = shoppingCartList.FirstOrDefault(p => p.ProductId == id);
            shoppingCartList.Remove(prodToRemove);

            this.HttpContext.Session.Set(GlobalConstants.SessionCart, shoppingCartList);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
