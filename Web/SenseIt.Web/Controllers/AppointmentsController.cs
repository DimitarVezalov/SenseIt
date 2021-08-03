namespace SenseIt.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data;
    using SenseIt.Web.ViewModels.Appointments;
    using SenseIt.Web.ViewModels.AppServices;

    [Authorize]
    public class AppointmentsController : BaseController
    {
        private readonly IAppServicesService appServicesService;
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentsController(
            IAppServicesService appServicesService,
            UserManager<ApplicationUser> userManager)
        {
            this.appServicesService = appServicesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var appService = await this.appServicesService
                .GetAppServiceById<AppointmentAppServiceViewModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            var model = new AppointmentIndexViewModel
            {
                AppService = appService,
                Username = user.UserName,
            };

            return this.View(model);
        }
    }
}
