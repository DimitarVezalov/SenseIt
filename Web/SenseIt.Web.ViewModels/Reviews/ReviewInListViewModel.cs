namespace SenseIt.Web.ViewModels.Reviews
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class ReviewInListViewModel : IMapFrom<Review>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string PostedOn { get; set; }

        public string PostedBy { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Review, ReviewInListViewModel>()
                .ForMember(x => x.PostedBy, opt =>
                opt.MapFrom(y => y.PostedBy.UserName))
                .ForMember(x => x.PostedOn, opt =>
                opt.MapFrom(y => y.CreatedOn.ToShortDateString()));
        }
    }
}
