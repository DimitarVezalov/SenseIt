<<<<<<< HEAD
﻿namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Services.Data.Admin.Models.Categories;

=======
﻿using Microsoft.AspNetCore.Mvc;
using SenseIt.Services.Data.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenseIt.Web.Areas.Administration.Controllers
{
>>>>>>> 19e807e1cd0783dbe1df90bb29b306f95b5ab0fc
    public class ProductCategoriesController : AdministrationController
    {
        private readonly IAdminProductCategoriesService productCategoriesService;

        public ProductCategoriesController(IAdminProductCategoriesService productCategoriesService)
        {
            this.productCategoriesService = productCategoriesService;
        }

<<<<<<< HEAD
        public IActionResult Add()
        {
            return this.View("AddEdit");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var result = await this.productCategoriesService.CreateAsync(input.Name);

            return this.RedirectToAction(nameof(this.All));
        }

=======
>>>>>>> 19e807e1cd0783dbe1df90bb29b306f95b5ab0fc
        public async Task<IActionResult> All()
        {
            var categories = await this.productCategoriesService.GetCategoriesList();

            return this.View(categories);
        }
<<<<<<< HEAD

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            return this.View("AddEdit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, CategoryEditModel input)
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
=======
>>>>>>> 19e807e1cd0783dbe1df90bb29b306f95b5ab0fc
    }
}
