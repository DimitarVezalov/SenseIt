using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenseIt.Web.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult ProductsAll()
        {
            return this.View();
        }
    }
}
