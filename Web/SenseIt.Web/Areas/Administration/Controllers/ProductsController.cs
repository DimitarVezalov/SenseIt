namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Web.Utility;
    using SenseIt.Web.ViewModels.Admin.Product;

    public class ProductsController : AdministrationController
    {
        private readonly IAdminProductsService adminProductsService;
        private readonly IAdminProductCategoriesService adminCategoriesService;
        private readonly Cloudinary cloudinary;

        public ProductsController(
            IAdminProductsService adminProductsService,
            IAdminProductCategoriesService adminCategoriesService,
            Cloudinary cloudinary)
        {
            this.adminProductsService = adminProductsService;
            this.adminCategoriesService = adminCategoriesService;
            this.cloudinary = cloudinary;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var products = await this.adminProductsService.GetAllProductsAsync<AdminProductsListingViewModel>();

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
        public async Task<IActionResult> Add(ICollection<IFormFile> files, CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var imageUrl = await CloudinaryHelper.Upload(this.cloudinary, files);

            await this.adminProductsService.CreateAsync(input.Category, input.Name, imageUrl,
                input.Description, input.Brand, input.Gender, input.InStockQuantity, input.Price);

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

            var model = await this.adminProductsService.GetDetailsModel<AdminProductDetailsViewModel>(id);

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

            var product = await this.adminProductsService.GetProductById<AdminProductUpdateFormModel>(id);
            product.Categories = categoris;
            product.Genders = genders;

            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ICollection<IFormFile> files, AdminProductUpdateModel input, int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.Edit)}/{id}");
            }

            var imageUrl = files.Any() ? await CloudinaryHelper.Upload(this.cloudinary, files) : null;

            var isUpdated = await this.adminProductsService.Update(id, input.Name, input.Description,
                input.CategoryName, imageUrl, input.Brand, input.Gender, input.Price);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
