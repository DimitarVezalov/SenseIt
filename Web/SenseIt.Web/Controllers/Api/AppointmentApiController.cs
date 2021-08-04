namespace SenseIt.Web.Controllers.Api
{
    using System;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data;
    using SenseIt.Web.ViewModels.Api;
    using SenseIt.Web.ViewModels.Appointments;

    using static SenseIt.Common.GlobalConstants;

    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly string loginUserId;

        public AppointmentApiController(
            IAppointmentsService appointmentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.appointmentsService = appointmentsService;
            this.userManager = userManager;
            this.loginUserId = this.userManager.GetUserId(this.User);
        }

        [HttpPost]
        [Route("SaveAppointmentData")]
        public IActionResult SaveAppointmentData(CreateAppointmentInputModel input)
        {
            return this.Ok();
        }
    }
}
