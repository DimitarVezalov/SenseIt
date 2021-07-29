namespace SenseIt.Web.ViewModels.Admin.AppServices
{
    using System;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AppServiceDetailsModel : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public string DurationToString => this.Duration.ToString().Substring(0, 5);

        public string DurationAsText => this.GetDurationText();

        private string GetDurationText()
        {
            var hoursText = this.DurationToString[1] == '1' ? "hour" : "hours";
            var minutesText = this.DurationToString.Substring(3, 2) == "00" ? string.Empty : "minutes";
            var minutesDigits = this.DurationToString.Substring(3, 2) == "00" ? string.Empty : this.DurationToString.Substring(3, 2);
            minutesText = minutesDigits == "01" ? "minute" : "minutes";
            var minutesDigitsAndText = minutesDigits == string.Empty ? string.Empty : $"and {minutesDigits} {minutesText}";

            var durationText = $"{this.DurationToString.Substring(1, 1)} {hoursText} {minutesDigitsAndText}";

            return durationText;
        }
    }
}
