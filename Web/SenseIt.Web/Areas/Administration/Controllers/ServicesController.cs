namespace SenseIt.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Services.Data.Admin.Models.AppServices;

    public class ServicesController : AdministrationController
    {
        private readonly IAdminAppServicesService adminAppServicesService;

        public ServicesController(IAdminAppServicesService adminAppServicesService)
        {
            this.adminAppServicesService = adminAppServicesService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var services = await this.adminAppServicesService.GetAllServicesAsync();

            return this.View(services);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateServiceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }

            var result = await this.adminAppServicesService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
