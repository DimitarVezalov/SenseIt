﻿namespace SenseIt.Web.Controllers
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
        private readonly IAppointmentsService appointmentsService;
        private readonly IAppServicesService appServicesService;
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentsController(
            IAppointmentsService appointmentsService,
            IAppServicesService appServicesService,
            UserManager<ApplicationUser> userManager)
        {
            this.appointmentsService = appointmentsService;
            this.appServicesService = appServicesService;
            this.userManager = userManager;
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
        public async Task<IActionResult> Add(CreateAppointmentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Error();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var result = await this.appointmentsService.CreateAsync(
                                                    user.Id,
                                                    input.ServiceId,
                                                    input.StartDate,
                                                    input.CustomerFullName,
                                                    input.CustomerAge,
                                                    input.AdditionalNotes);

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
