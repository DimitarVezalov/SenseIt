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

    using static SenseIt.Common.GlobalConstants.Pagination;

    public class StoreController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IProductCategoriesService categoriesService;

        public StoreController(IProductsService productsService, IProductCategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        [ActionName("All")]
        public async Task<IActionResult> ProductsAll(int id = DefaultStartingPage)
        {
            var products = await this.productsService.GetAllPaging<ProductInListViewModel>(id, ProductsPerPage);
            var productsCount = this.productsService.GetCount();

            //var shoppingCartList = this.GetShoppingCartList();
            //this.SetExistsInCart(products, shoppingCartList);

            var categories = await this.categoriesService.GetProductCategories();
            var genders = this.productsService.GetGenders();

            var viewModel = new ProductsPagingViewModel
            {
                ProductsPerPage = ProductsPerPage,
                PageNumber = id,
                ProductsCount = productsCount,
                Products = products,
                Categories = categories,
                Genders = genders,
            };

            return this.View(viewModel);
        }

        [ActionName("Details")]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return this.Error();
            }

            var productModel = await this.productsService
                .GetDetails<ProductDetailsViewModel>(id);
            productModel.ExistsInCart = false;

            var relatedProducts = await this.productsService
                .GetAllByCategory<ProductInListViewModel>(productModel.CategoryName, productModel.Id);

            productModel.Products = relatedProducts;

            //var shoppingCartList = this.GetShoppingCartList();

            //foreach (var item in shoppingCartList)
            //{
            //    if (item.ProductId == id)
            //    {
            //        productModel.ExistsInCart = true;
            //    }
            //}

            //this.SetExistsInCart(relatedProducts, shoppingCartList);

            return this.View(productModel);
        }

        [ActionName("ByGender")]
        public async Task<IActionResult> AllByGender(string gender, int id = DefaultStartingPage)
        {
            if (gender == null)
            {
                return this.Error();
            }

            var products = await this.productsService.GetByGenderPaging<ProductInListViewModel>(id, ProductsPerPage, gender);
            var productsCount = await this.productsService.GetCount(gender);

            //var shoppingCartList = this.GetShoppingCartList();
            //this.SetExistsInCart(products, shoppingCartList);

            var categories = await this.categoriesService.GetProductCategories();
            var genders = this.productsService.GetGenders();

            var viewModel = new ProductsPagingByGenderViewModel
            {
                ProductsPerPage = ProductsPerPage,
                PageNumber = id,
                ProductsCount = productsCount,
                Products = products,
                Categories = categories,
                Genders = genders,
            };

            if (this.Request.Query.ContainsKey("gender"))
            {
                viewModel.Gender = this.Request.Query["gender"];
            }

            return this.View(viewModel);
        }

        [ActionName("ByCategory")]
        public async Task<IActionResult> AllByCategory(string category, int id = DefaultStartingPage)
        {
            if (category == null)
            {
                return this.Error();
            }

            var products = await this.productsService.GetByCategoryPaging<ProductInListViewModel>(id, ProductsPerPage, category);
            var productsCount = await this.productsService.GetCount(category);

            //var shoppingCartList = this.GetShoppingCartList();
            //this.SetExistsInCart(products, shoppingCartList);

            var categories = await this.categoriesService.GetProductCategories();
            var genders = this.productsService.GetGenders();

            var viewModel = new ProductsPagingByCategoryViewModel
            {
                ProductsPerPage = ProductsPerPage,
                PageNumber = id,
                ProductsCount = productsCount,
                Products = products,
                Categories = categories,
                Genders = genders,
            };

            if (this.Request.Query.ContainsKey("category"))
            {
                viewModel.Category = this.Request.Query["category"];
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Search(string keyword, int id = DefaultStartingPage)
        {
            var productsCount = 0;
            IEnumerable<ProductInListViewModel> products = null;

            if (keyword == null)
            {
                products = await this.productsService.GetAllPaging<ProductInListViewModel>(id, ProductsPerPage);
                productsCount = this.productsService.GetCount();
            }
            else
            {
                products = await this.productsService.GetBySearchTermPaging<ProductInListViewModel>(id, ProductsPerPage, keyword);
                productsCount = await this.productsService.GetCount(keyword);
            }

            //var shoppingCartList = this.GetShoppingCartList();
            //this.SetExistsInCart(products, shoppingCartList);

            var categories = await this.categoriesService.GetProductCategories();
            var genders = this.productsService.GetGenders();

            var viewModel = new ProductsPagingBySearchTermViewModel
            {
                ProductsPerPage = ProductsPerPage,
                PageNumber = id,
                ProductsCount = productsCount,
                Products = products,
                Categories = categories,
                Genders = genders,
                Keyword = keyword,
            };

            return this.View(viewModel);
        }


        //public IActionResult AddToCart(int id, int? quantity = 1)
        //{
        //    List<ShoppingCart> shoppingCartList = this.GetShoppingCartList();

        //    if (!shoppingCartList.Select(x => x.ProductId).Contains(id))
        //    {
        //        shoppingCartList.Add(new ShoppingCart { ProductId = id, Quantity = quantity ?? 1 });
        //    }

        //    this.HttpContext.Session.Set(GlobalConstants.SessionCart, shoppingCartList);

        //    return this.RedirectToAction("All");
        //}

        //private List<ShoppingCart> GetShoppingCartList()
        //{
        //    List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

        //    if (this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart) != null
        //        && this.HttpContext.Session.Get<IEnumerable<ShoppingCart>>(GlobalConstants.SessionCart).Count() > 0)
        //    {
        //        shoppingCartList = this.HttpContext.Session.Get<List<ShoppingCart>>(GlobalConstants.SessionCart);
        //    }

        //    return shoppingCartList;
        //}

        //private void SetExistsInCart(IEnumerable<ProductInListViewModel> products, List<ShoppingCart> shoppingCartList)
        //{
        //    foreach (var item in shoppingCartList)
        //    {
        //        foreach (var prod in products)
        //        {
        //            if (item.ProductId == prod.Id)
        //            {
        //                prod.ExistsInCart = true;
        //            }
        //        }
        //    }
        //}
    }
}
