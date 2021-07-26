﻿namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Web.ViewModels.Admin.Product;

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
        public async Task<IActionResult> Add(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            await this.adminProductsService.CreateAsync(input.Category, input.Name, input.ImageUrl,
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

            var isUpdated = await this.adminProductsService.Update(id, input.Name, input.Description,
                input.CategoryName, input.ImageUrl, input.Brand, input.Gender, input.Price);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
