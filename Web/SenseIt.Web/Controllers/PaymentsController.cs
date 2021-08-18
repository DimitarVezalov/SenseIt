namespace SenseIt.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data;
    using SenseIt.Web.ViewModels.Payments;

    [Authorize]
    public class PaymentsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;

        public PaymentsController(
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var isCartEmpty = await this.usersService.IsCartEmpty(user.Id);
            if (!isCartEmpty)
            {
                return this.RedirectToAction("All", "Store");
            }

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

            var user = await this.userManager.GetUserAsync(this.User);

            if (user.PhoneNumber == null || user.PhoneNumber != input.PhoneNumber)
            {
                var update = await this.usersService.SetPhoneNumber(user.Id, input.PhoneNumber);
            }

            var orderId = await this.ordersService
                .CreateOrder(user.Id, input.Town, input.Street, input.Number, input.ZipCode);

            return this.RedirectToAction("Index", "Orders", new { id = orderId });
        }
    }
}
