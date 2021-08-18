namespace SenseIt.Web.Controllers
{
    using System.Globalization;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Common;
    using SenseIt.Services.Data;
    using SenseIt.Services.Messaging;
    using SenseIt.Web.Utility;
    using SenseIt.Web.ViewModels.Orders;

    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrdersService ordersService;
        private readonly IEmailSender emailSender;
        private readonly ICartsService cartsService;
        private readonly IUsersService usersService;

        public OrdersController(
            IOrdersService ordersService,
            IEmailSender emailSender,
            ICartsService cartsService,
            IUsersService usersService)
        {
            this.ordersService = ordersService;
            this.emailSender = emailSender;
            this.cartsService = cartsService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var model = await this.ordersService.GetById<OrderDetailsViewModel>(id);

            var emailModel = await this.ordersService.GetById<EmailOrderViewModel>(id);

            var productsSb = new StringBuilder();
            foreach (var item in emailModel.OrderItems)
            {
                productsSb.AppendLine($"Name: {item.Name}     Qty:{item.Quantity}   Price: ${item.Price.ToString("F2")} ");
                productsSb.Append("<br>");
            }

            var html = EmailSenderHelper.PrepareOrderHtml();
            var content = string.Format(
                html,
                emailModel.RecipientFullName,
                emailModel.Id,
                productsSb.ToString().TrimEnd(),
                emailModel.TotalSum.ToString("F2"),
                emailModel.CreatedOn.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));

            await this.emailSender.SendEmailAsync(
                                                    "wopopo13@gmail.com",
                                                    GlobalConstants.SystemName,
                                                    "geveye5549@asmm5.com",
                                                    emailModel.Id.ToString(),
                                                    content);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var removeAll = await this.cartsService.RemoveAll(userId);

            return this.View(model);
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await this.ordersService
                .GetAllByUser<OrderInListViewModel>(userId);

            return this.View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipientId = await this.usersService.GetUserIdByOrder(id);

            if (userId != recipientId)
            {
                return this.Error();
            }

            var orders = await this.ordersService
                .GetAllByUser<OrderInListViewModel>(userId);

            return this.View(orders);
        }
    }
}
