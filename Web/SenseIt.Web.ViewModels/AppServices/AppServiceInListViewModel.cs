namespace SenseIt.Web.ViewModels.AppServices
{
    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppServiceInListViewModel : IMapFrom<Service>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public string DurationAsText => this.GetDurationText();

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int ReviewsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Service, AppServiceInListViewModel>()
                .ForMember(x => x.Duration, opt =>
                opt.MapFrom(y => y.Duration.ToString().Substring(0, 5)));
        }

        private string GetDurationText()
        {
            var hoursText = this.Duration[1] == '1' ? "hour" : "hours";
            var minutesText = this.Duration.Substring(3, 2) == "00" ? string.Empty : "minutes";
            var minutesDigits = this.Duration.Substring(3, 2) == "00" ? string.Empty : this.Duration.Substring(3, 2);
            minutesText = minutesDigits == "01" ? "minute" : "minutes";
            var minutesDigitsAndText = minutesDigits == string.Empty ? string.Empty : $"and {minutesDigits} {minutesText}";

            var durationText = $"{this.Duration.Substring(1, 1)} {hoursText} {minutesDigitsAndText}";

            return durationText;
        }
    }
}
