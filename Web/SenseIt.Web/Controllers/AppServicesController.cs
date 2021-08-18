namespace SenseIt.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SenseIt.Services.Data;
    using SenseIt.Web.ViewModels.AppServices;

    public class AppServicesController : BaseController
    {
        private readonly IAppServiceCategoriesService categoriesService;
        private readonly IAppServicesService appServicesService;

        public AppServicesController(
            IAppServiceCategoriesService categoriesService,
            IAppServicesService appServicesService)
        {
            this.categoriesService = categoriesService;
            this.appServicesService = appServicesService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await this.categoriesService.GetAllCategories<AppServicesCategoriesViewModel>();

            var indexModel = new AppServicesIndexViewModel
            {
                AllCategories = categories,
            };

            return this.View(indexModel);
        }

        public async Task<IActionResult> ByCategory(int? id)
        {
            if (id == null)
            {
                return this.Error();
            }

            var appServices = await this.appServicesService.GetAllByCategory<AppServiceInListViewModel>(id);

            var viewModel = new AllAppServicesVIewModel { AppServices = appServices };

            return this.View(viewModel);
        }
    }
}
