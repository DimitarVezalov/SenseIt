using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SenseIt.Data.Models;
using SenseIt.Web.ViewModels.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SenseIt.Web.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public PaymentsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var firstName = user.FirstName;
            var lastName = user.LastName;
            var phoneNumber = user.PhoneNumber;
            var email = user.Email;

            var model = new PaymentDetailsFormModel
            {
                FullName = $"{firstName} {lastName}",
                Email = email,
                PhoneNumber = phoneNumber,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Checkout(PaymentDetailsFormModel input)
        {
            
        }
    }
}
