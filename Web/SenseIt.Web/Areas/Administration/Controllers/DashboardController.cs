using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenseIt.Web.Areas.Administration.Controllers
{
    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
