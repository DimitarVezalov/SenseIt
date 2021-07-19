namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Services.Data.Admin.Models.AppServices;

    public class ServicesController : AdministrationController
    {
        private readonly IAdminAppServicesService adminAppServicesService;
        private readonly IAdminServiceCategoriesService categoriesService;

        public ServicesController(
            IAdminAppServicesService adminAppServicesService,
            IAdminServiceCategoriesService categoriesService)
        {
            this.adminAppServicesService = adminAppServicesService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var services = await this.adminAppServicesService.GetAllServicesAsync();

            return this.View(services);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await this.categoriesService.GetCategoryNames();

            var model = new CreateAppServiceInputModel
            {
                Categories = categories,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateAppServiceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var result = await this.adminAppServicesService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var service = await this.adminAppServicesService.GetDetailsModel(id);

            if (service == null)
            {
                return this.NotFound();
            }

            return this.View(service);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = await this.adminAppServicesService.GetServiceById(id);
            var categories = await this.categoriesService.GetCategoryNames();
            model.Categories = categories;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, EditAppServiceInputModel input)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.Edit)}/{id}");
            }

            var result = await this.adminAppServicesService.Update(id, input);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var result = await this.adminAppServicesService.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Undelete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var result = await this.adminAppServicesService.Undelete(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
