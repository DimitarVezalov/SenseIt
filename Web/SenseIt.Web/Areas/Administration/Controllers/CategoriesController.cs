namespace SenseIt.Web.Areas.Administration.Controllers
{
<<<<<<< HEAD
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;

    public class CategoriesController : AdministrationController
    {

=======
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
>>>>>>> 19e807e1cd0783dbe1df90bb29b306f95b5ab0fc

        public IActionResult Index()
        {
            return this.View();
        }
<<<<<<< HEAD
=======

        public async Task<IActionResult> ProductCategories()
        {
            var productCategories = await this.productCategoriesService.GetCategoriesList();

            return this.View(productCategories);
        }
>>>>>>> 19e807e1cd0783dbe1df90bb29b306f95b5ab0fc
    }
}
