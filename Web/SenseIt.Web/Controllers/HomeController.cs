namespace SenseIt.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Messaging;
    using SenseIt.Web.ViewModels.Contact;

    public class HomeController : BaseController
    {
        private readonly IEmailSender emailSender;

        public HomeController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult AboutUs()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactEmailFormModel input)
        {
            this.emailSender.SendEmailAsync(input.Email, input.Name, "jixole6248@ampswipe.com", input.Subject, input.Message);
            return this.View();
        }
    }
}
