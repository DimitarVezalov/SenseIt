namespace SenseIt.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data;
    using SenseIt.Web.ViewModels.Products;

    using static SenseIt.Common.GlobalConstants.Pagination;

    public class StoreController : BaseController
    {
        private readonly IProductsService productsService;

        public StoreController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [ActionName("All")]
        public async Task<IActionResult> ProductsAll(int id = DefaultStartingPage)
        {
            var products = await this.productsService.GetAll<ProductInListViewModel>(id, ProductsPerPage);
            var productsCount = this.productsService.GetCount();

            var viewModel = new ProductsPagingViewModel
            {
                ProductsPerPage = ProductsPerPage,
                PageNumber = id,
                ProductsCount = productsCount,
                Products = products,
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
    }
}
