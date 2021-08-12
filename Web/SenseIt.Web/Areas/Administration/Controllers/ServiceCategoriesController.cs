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
    using SenseIt.Web.ViewModels.Admin.Categories.AppServiceCategory;

    using static SenseIt.Common.GlobalConstants;

    public class ServiceCategoriesController : AdministrationController
    {
        private readonly IAdminServiceCategoriesService serviceCategoriesService;
        private readonly Cloudinary cloudinary;

        public ServiceCategoriesController(
            IAdminServiceCategoriesService serviceCategoriesService,
            Cloudinary cloudinary)
        {
            this.serviceCategoriesService = serviceCategoriesService;
            this.cloudinary = cloudinary;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ICollection<IFormFile> files, AppServiceCategoryAddEditModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var imageUrl = await CloudinaryHelper
                .Upload(this.cloudinary, files, CloudinaryConstants.ServiceCategoriesFolderName);

            var result = await this.serviceCategoriesService.CreateAsync(input.Name, input.Description, imageUrl);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var categories = await this.serviceCategoriesService.GetCategoriesList<AppServiceCategoriesListingModel>();

            return this.View(categories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.serviceCategoriesService.GetEditModel<AppServiceCategoryAddEditModel>(id);

            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, ICollection<IFormFile> files, AppServiceCategoryAddEditModel input)
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
                .Upload(this.cloudinary, files, CloudinaryConstants.ServiceCategoriesFolderName)
                : null;

            var result = await this.serviceCategoriesService.Edit(id, input.Name, input.Description, imageUrl);

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
