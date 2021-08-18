namespace SenseIt.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
