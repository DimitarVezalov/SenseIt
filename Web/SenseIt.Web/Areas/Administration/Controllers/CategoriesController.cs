namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;

    public class CategoriesController : AdministrationController
    {


        public IActionResult Index()
        {
            return this.View();
        }
    }
}
