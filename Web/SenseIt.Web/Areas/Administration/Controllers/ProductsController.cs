namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Services.Data.Admin.Models.Product;

    public class ProductsController : AdministrationController
    {
        private readonly IAdminProductsService adminProductsService;
        private readonly IAdminProductCategoriesService adminCategoriesService;

        public ProductsController(IAdminProductsService adminProductsService, IAdminProductCategoriesService adminCategoriesService)
        {
            this.adminProductsService = adminProductsService;
            this.adminCategoriesService = adminCategoriesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var products = await this.adminProductsService.GetAllProductsAsync();

            return this.View(products);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await this.adminCategoriesService.GetCategoryNames();
            var genders = this.adminProductsService.GetGendersList();
            var model = new CreateProductInputModel
            {
                Categories = categories,
                Genders = genders,
            };
            return this.View(model);
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

        public async Task<IActionResult> Restock(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var isRestocked = await this.adminProductsService.Restock(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var isDeleted = await this.adminProductsService.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var isDeleted = await this.adminProductsService.Undelete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = await this.adminProductsService.GetDetailsModel(id);

            return this.View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var categoris = await this.adminCategoriesService.GetCategoryNames();
            var genders = this.adminProductsService.GetGendersList();

            var product = await this.adminProductsService.GetProductById(id);
            product.Categories = categoris;
            product.Genders = genders;

            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminProductUpdateModel input, int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.Edit)}/{id}");
            }

            var isUpdated = await this.adminProductsService.Update(id, input);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
