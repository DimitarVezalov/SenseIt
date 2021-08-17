using Microsoft.AspNetCore.Mvc;
using SenseIt.Web.ViewModels.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenseIt.Web.Controllers
{
    public class PaymentsController : BaseController
    {
        public IActionResult Checkout()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Checkout(PaymentDetailsFormModel input)
        {

        }
    }
}
