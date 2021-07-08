using Microsoft.AspNetCore.Mvc;
using SenseIt.Services.Data.Admin;
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
    }
}
