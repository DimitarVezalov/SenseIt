namespace SenseIt.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ServicesController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
