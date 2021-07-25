namespace SenseIt.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Services.Data.Admin.Models.Categories;
    using SenseIt.Services.Data.Admin.Models.Categories.AppServiceCategory;
    using System.Threading.Tasks;

    public class ServiceCategoriesController : AdministrationController
    {
        private readonly IAdminServiceCategoriesService serviceCategoriesService;

        public ServiceCategoriesController(IAdminServiceCategoriesService serviceCategoriesService)
        {
            this.serviceCategoriesService = serviceCategoriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AppServiceCategoryAddEditModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var result = await this.serviceCategoriesService.CreateAsync(input.Name, input.Description, input.ImageUrl);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var categories = await this.serviceCategoriesService.GetCategoriesList();

            return this.View(categories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.serviceCategoriesService.GetEditModel(id);

            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, AppServiceCategoryAddEditModel input)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.Edit)}/{id}");
            }

            var result = await this.serviceCategoriesService.Edit(id, input.Name, input.Description, input.ImageUrl);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var result = await this.serviceCategoriesService.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var result = await this.serviceCategoriesService.Undelete(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
