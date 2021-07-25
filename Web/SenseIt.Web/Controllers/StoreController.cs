namespace SenseIt.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data;
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

            var relatedProducts = await this.productsService
                .GetAllByCategory<ProductInListViewModel>(productModel.CategoryName, productModel.Id);

            productModel.Products = relatedProducts;

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
            var productsCount = this.productsService.GetCount(gender);

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
            var productsCount = this.productsService.GetCount(category);

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
    }
}
