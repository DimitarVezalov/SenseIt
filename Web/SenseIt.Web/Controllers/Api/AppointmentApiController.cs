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
            ApiCommonResponse<int> response = new ApiCommonResponse<int>();

            try
            {
                response.Status = this.appointmentsService.CreateUpdateAsync(
                   input.Id,
                   this.loginUserId,
                   input.ServiceId,
                   input.StartDate,
                   input.UserFullName,
                   input.UserAge,
                   input.AdditionalNotes).Result;

                if (response.Status == 1)
                {
                    response.Message = AppointmentConstants.AppointmentUpdated;
                }

                if (response.Status == 2)
                {
                    response.Message = AppointmentConstants.AppointmentAdded;
                }

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = AppointmentConstants.ErrorCode;
            }

            return this.Ok(response);
        }
    }
}
