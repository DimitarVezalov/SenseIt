using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SenseIt.Data.Models;
using SenseIt.Services.Data;
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
        private readonly IOrdersService ordersService;

        public PaymentsController(
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
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
        public async Task<IActionResult> Checkout(PaymentDetailsFormModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Error();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderId = await this.ordersService
                .CreateOrder(userId, input.Town, input.Street, input.Number, input.ZipCode);

            return this.RedirectToAction("Index", "Orders", new { id = orderId});
        }
    }
}
