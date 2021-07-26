namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Web.ViewModels.Admin.Categories;

    public class ProductCategoriesController : AdministrationController
    {
        private readonly IAdminProductCategoriesService productCategoriesService;

        public ProductCategoriesController(IAdminProductCategoriesService productCategoriesService)
        {
            this.productCategoriesService = productCategoriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductCategoryAddEditModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var result = await this.productCategoriesService.CreateAsync(input.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var categories = await this.productCategoriesService.GetCategoriesList<ProductCategoriesListingViewModel>();

            return this.View(categories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = await this.productCategoriesService.GetEditModel<ProductCategoryAddEditModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, ProductCategoryAddEditModel input)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.Edit)}/{id}");
            }

            var result = await this.productCategoriesService.Edit(id, input.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var result = await this.productCategoriesService.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var result = await this.productCategoriesService.Undelete(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
