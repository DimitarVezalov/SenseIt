using Microsoft.AspNetCore.Mvc;

namespace SenseIt.Web.Areas.Administration.Controllers
{
    public class ServiceCategoriesController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
