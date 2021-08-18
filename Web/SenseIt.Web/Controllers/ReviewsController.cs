namespace SenseIt.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
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
        private readonly IUsersService usersService;

        public ReviewsController(
            IReviewsService reviewsService,
            IAppServicesService appServicesService,
            UserManager<ApplicationUser> userManager,
            IUsersService usersService)
        {
            this.reviewsService = reviewsService;
            this.appServicesService = appServicesService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return this.Error();
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
        public async Task<IActionResult> Add(int? id, int rating, string content)
        {
            if (id == null || (rating < 1 || rating > 5) || content == null || (content.Length < 5 || content.Length > 1000))
            {
                return this.Error();
            }

            var userId = this.userManager.GetUserId(this.User);

            var result = await this.reviewsService.CreateAsync(id, userId, content, rating);

            return this.RedirectToAction(nameof(this.ServiceReviews), new { id });
        }

        [AllowAnonymous]
        public async Task<IActionResult> ServiceReviews(int? id)
        {
            if (id == null)
            {
                return this.Error();
            }

            var viewModel = await this.appServicesService
                .GetAppServiceById<AppServiceWithReviewsModel>(id);

            var modelReviews = await this.reviewsService
                .GetAllByAppService<ReviewInListViewModel>(id);

            viewModel.Reviews = modelReviews;
            viewModel.OverallRating = modelReviews.Any() ? (int)Math.Round(modelReviews.Average(r => r.Rating)) : 0;

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var reviews = await this.reviewsService
                .GetAllByUserId<UserReviewsViewModel>(userId);

            return this.View(reviews);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = await this.usersService.GetUserIdByReview(id);

            if (userId != customerId)
            {
                return this.Error();
            }

            var result = await this.reviewsService.Delete(id);

            if (!result)
            {
                return this.Error();
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
