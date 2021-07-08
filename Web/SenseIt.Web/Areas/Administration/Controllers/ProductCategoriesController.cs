using Microsoft.AspNetCore.Mvc;
using SenseIt.Services.Data.Admin;
using SenseIt.Services.Data.Admin.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenseIt.Web.Areas.Administration.Controllers
{
    public class ProductCategoriesController : AdministrationController
    {
        private readonly IAdminProductCategoriesService productCategoriesService;

        public ProductCategoriesController(IAdminProductCategoriesService productCategoriesService)
        {
            this.productCategoriesService = productCategoriesService;
        }

        public async Task<IActionResult> All()
        {
            var categories = await this.productCategoriesService.GetCategoriesList();

            return this.View(categories);
        }

        public async Task<IActionResult> Edit(int? id, ProductCategoryEditModel input)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"{nameof(this.Edit)}/{id}");
            }

            var result = this.productCategoriesService.Edit(id, input);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var categories = await this.productCategoriesService.GetCategoriesList();

            return this.View(categories);
        }
    }
}
