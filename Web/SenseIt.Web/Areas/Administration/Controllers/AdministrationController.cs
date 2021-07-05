namespace SenseIt.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Common;
    using SenseIt.Web.Controllers;

    [Authorize(Roles = GlobalConstants.Role.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
