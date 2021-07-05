namespace SenseIt.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Services.Data.Admin.Models;
    using System.Threading.Tasks;

    public class ProductsController : AdministrationController
    {
        private readonly IAdminProductsService adminProductsService;
        private readonly IAdminCategoriesService adminCategoriesService;

        public ProductsController(IAdminProductsService adminProductsService, IAdminCategoriesService adminCategoriesService)
        {
            this.adminProductsService = adminProductsService;
            this.adminCategoriesService = adminCategoriesService;
        }

        public async Task<IActionResult> All()
        {
            var products = await this.adminProductsService.GetAllProductsAsync();

            return this.View(products);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await this.adminCategoriesService.GetCategoryNames();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            await this.adminProductsService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.All));
        }

        //public async Task<IActionResult> Delete(int productId)
        //{
            
        //}
    }
}
