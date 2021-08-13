namespace SenseIt.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data;
    using SenseIt.Services.Messaging;
    using SenseIt.Web.Utility;
    using SenseIt.Web.ViewModels.Appointments;
    using SenseIt.Web.ViewModels.AppServices;

    [Authorize]
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IAppServicesService appServicesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public AppointmentsController(
            IAppointmentsService appointmentsService,
            IAppServicesService appServicesService,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            this.appointmentsService = appointmentsService;
            this.appServicesService = appServicesService;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return this.Error();
            }

            var appService = await this.appServicesService
                .GetAppServiceById<AppointmentAppServiceViewModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            var activeApointments = await this.appointmentsService
                .GetAllByUserId<AppointmentInModalDetailsVIewModel>(user.Id);

            var model = new AppointmentIndexViewModel
            {
                AppService = appService,
                Username = user.UserName,
                ActiveAppointments = activeApointments,
            };

            return this.View(model);
        }

        [HttpPost]
        [ActionName("Book")]
        public async Task<IActionResult> BookAppointment(CreateAppointmentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Error();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var appointmentId = await this.appointmentsService.CreateAsync(
                                                    user.Id,
                                                    input.ServiceId,
                                                    input.StartDate,
                                                    input.CustomerFullName,
                                                    input.CustomerAge,
                                                    input.AdditionalNotes);

            var appointment = await this.appointmentsService
                .GetAppointmentById<EmailAppointmentViewModel>(appointmentId);

            var appUser = await this.userManager.GetUserAsync(this.User);

            var html = EmailSenderHelper.PrepareHtml();
            var content = string.Format(
                html,
                appUser.UserName,
                appointment.ServiceImageUrl,
                appointment.CustomerFullName,
                appointment.ServiceName,
                appointment.ServiceDuration,
                appointment.StartDate);

            await this.emailSender.SendEmailAsync("wopopo13@gmail.com", "Sense It", "geveye5549@asmm5.com", $"{appointment.ServiceName}", content);

            return this.RedirectToAction(nameof(this.Index), new { id = input.ServiceId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int? id, int? appServiceId)
        {
            if (id == null || appServiceId == null)
            {
                return this.Error();
            }

            var result = await this.appointmentsService.CancelAppointment(id);

            return this.RedirectToAction(nameof(this.Index), new { id = appServiceId });
        }
    }
}
