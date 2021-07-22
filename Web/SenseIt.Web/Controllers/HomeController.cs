namespace SenseIt.Web.Controllers
{
    using System.Diagnostics;

    using SenseIt.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }  
    }
}
