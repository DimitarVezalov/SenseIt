namespace SenseIt.Services.Data.Admin.Models.AppServices
{
    public class AppServiceDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string Duration { get; set; }

        public string DurationAsText => this.GetDurationText();

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
