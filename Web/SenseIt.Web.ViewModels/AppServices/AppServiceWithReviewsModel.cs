namespace SenseIt.Web.ViewModels.AppServices
{
    using System.Collections.Generic;

    using SenseIt.Web.ViewModels.Reviews;

    public class AppServiceWithReviewsModel : AppServiceInListViewModel
    {
        public IEnumerable<ReviewInListViewModel> Reviews { get; set; }
    }
}
