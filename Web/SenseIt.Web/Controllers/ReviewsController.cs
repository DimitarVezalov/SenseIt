namespace SenseIt.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SenseIt.Data.Models;
    using SenseIt.Services.Data;
    using SenseIt.Web.ViewModels.AppServices;
    using SenseIt.Web.ViewModels.Reviews;

    [Authorize]
    public class ReviewsController : BaseController
    {
        private readonly IReviewsService reviewsService;
        private readonly IAppServicesService appServicesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(
            IReviewsService reviewsService,
            IAppServicesService appServicesService,
            UserManager<ApplicationUser> userManager)
        {
            this.reviewsService = reviewsService;
            this.appServicesService = appServicesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var modelAppService = await this.appServicesService.GetAppServiceById<AppServiceInListViewModel>(id);

            var model = new CreateReviewInputModel
            {
                AppService = modelAppService,
                AppServiceId = modelAppService.Id,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, CreateReviewInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Error();
            }

            var userId = this.userManager.GetUserId(this.User);

            var result = await this.reviewsService.CreateAsync(id, userId, input.Content, input.Rating);

            return this.RedirectToAction(nameof(this.ServiceReviews), new { id });
        }

        [AllowAnonymous]
        public async Task<IActionResult> ServiceReviews(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = await this.appServicesService
                .GetAppServiceById<AppServiceWithReviewsModel>(id);

            var modelReviews = await this.reviewsService
                .GetAllByAppService<ReviewInListViewModel>(id);

            viewModel.Reviews = modelReviews;

            return this.View(viewModel);
        }
    }
}
