namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Web.Utility;
    using SenseIt.Web.ViewModels.Admin.AppServices;

    using static SenseIt.Common.GlobalConstants;

    public class AppServicesController : AdministrationController
    {
        private readonly IAdminAppServicesService adminAppServicesService;
        private readonly IAdminServiceCategoriesService categoriesService;
        private readonly Cloudinary cloudinary;

        public AppServicesController(
            IAdminAppServicesService adminAppServicesService,
            IAdminServiceCategoriesService categoriesService,
            Cloudinary cloudinary)
        {
            this.adminAppServicesService = adminAppServicesService;
            this.categoriesService = categoriesService;
            this.cloudinary = cloudinary;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var services = await this.adminAppServicesService
                .GetAllAppServicesAsync<AdminAppServiceListingViewModel>();

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
        public async Task<IActionResult> Add(ICollection<IFormFile> files, CreateAppServiceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var imageUrl = await CloudinaryHelper
                .Upload(this.cloudinary, files, CloudinaryConstants.ServicesFolderName);

            var result = await this.adminAppServicesService
                .CreateAsync(input.Name, input.Description, input.Duration, input.Category, imageUrl, input.Price);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var service = await this.adminAppServicesService
                .GetDetailsModel<AppServiceDetailsModel>(id);

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

            var model = await this.adminAppServicesService.GetServiceById<EditAppServiceFormModel>(id);
            var categories = await this.categoriesService.GetCategoryNames();
            model.Categories = categories;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, ICollection<IFormFile> files, EditAppServiceInputModel input)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.Edit)}/{id}");
            }

            var imageUrl = files.Any() ?
                await CloudinaryHelper
                .Upload(this.cloudinary, files, CloudinaryConstants.ServicesFolderName)
                : null;

            var result = await this.adminAppServicesService
                .Update(id, input.Name, input.Description, imageUrl, input.Category, input.Duration, input.Price);

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
