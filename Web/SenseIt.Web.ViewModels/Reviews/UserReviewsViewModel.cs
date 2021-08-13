namespace SenseIt.Web.ViewModels.Reviews
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class UserReviewsViewModel : IMapFrom<Review>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AppServiceImageUrl { get; set; }

        public string AppServiceName { get; set; }

        public string Content { get; set; }

        public string PostedOn { get; set; }

        public string PostedBy { get; set; }

        public int Rating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Review, UserReviewsViewModel>()
                .ForMember(x => x.PostedBy, opt =>
                opt.MapFrom(y => y.PostedBy.UserName))
                .ForMember(x => x.PostedOn, opt =>
                opt.MapFrom(y => y.CreatedOn.ToShortDateString()));
        }
    }
}
