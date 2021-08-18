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

    using SenseIt.Common;
    using System.Security.Claims;

    [Authorize]
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IAppServicesService appServicesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly IUsersService usersService;

        public AppointmentsController(
            IAppointmentsService appointmentsService,
            IAppServicesService appServicesService,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IUsersService usersService)
        {
            this.appointmentsService = appointmentsService;
            this.appServicesService = appServicesService;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Booking(int? id)
        {
            if (id == null)
            {
                return this.Error();
            }

            var appService = await this.appServicesService
                .GetAppServiceById<AppointmentAppServiceViewModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            var lastAppointmentDate = await this.appointmentsService
                .GetLastAppointmentStartDate(user.Id);

            var days = (DateTime.UtcNow - lastAppointmentDate).Days;
            var hasAppointments = await this.usersService.HasAppointments(user.Id);

            var activeApointments = await this.appointmentsService
                .GetAllInModal<AppointmentInModalDetailsVIewModel>(user.Id);

            var model = new AppointmentIndexViewModel
            {
                AppService = appService,
                Username = user.UserName,
                ActiveAppointments = activeApointments,
                LastBookedPastDays = days,
                HasAppointments = hasAppointments,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Booking(CreateAppointmentInputModel input)
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

            var html = EmailSenderHelper.PrepareAppointmentHtml();
            var content = string.Format(
                html,
                appUser.UserName,
                appointment.ServiceImageUrl,
                appointment.CustomerFullName,
                appointment.ServiceName,
                appointment.ServiceDuration,
                appointment.StartDate);

            await this.emailSender.SendEmailAsync("wopopo13@gmail.com", GlobalConstants.SystemName, "geveye5549@asmm5.com", appointment.ServiceName, content);

            return this.RedirectToAction(nameof(this.Booking), new { id = input.ServiceId });
        }

        public async Task<IActionResult> AllActive()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var appointments = await this.appointmentsService
                .GetAllActiveByUser<UserAppointmentsListViewModel>(userId);

            return this.View(appointments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = await this.usersService.GetUserIdByAppointment(id);

            if (userId != customerId)
            {
                return this.Error();
            }

            var appointment = await this.appointmentsService
                .GetAppointmentById<UserAppointmentViewModel>(id);

            if (appointment == null)
            {
                return this.Error();
            }

            return this.View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = await this.usersService.GetUserIdByAppointment(id);

            if (userId != customerId)
            {
                return this.Error();
            }

            var result = await this.appointmentsService.CancelAppointment(id);

            return this.RedirectToAction(nameof(this.AllActive));
        }
    }
}
