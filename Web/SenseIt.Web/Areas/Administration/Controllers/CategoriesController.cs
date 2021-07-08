namespace SenseIt.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using System.Threading.Tasks;

    public class CategoriesController : AdministrationController
    {
        private readonly IAdminProductCategoriesService productCategoriesService;

        public CategoriesController(IAdminProductCategoriesService productCategoriesService)
        {
            this.productCategoriesService = productCategoriesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> ProductCategories()
        {
            var productCategories = await this.productCategoriesService.GetCategoriesList();

            return this.View(productCategories);
        }
    }
}
