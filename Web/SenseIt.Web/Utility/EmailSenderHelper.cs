namespace SenseIt.Web.Utility
{
    using System.Text;

    public static class EmailSenderHelper
    {
        public static string PrepareHtml()
        {
            var htmlSb = new StringBuilder();
            htmlSb.AppendLine("<h1 class=\"text-center text-danger\">Dear {0}</h1>");
            htmlSb.AppendLine("<br />");
            htmlSb.AppendLine("<h2>You have booked appointment at Sense It Beauty Studio for one of our services.</h2>");
            htmlSb.AppendLine("<hr />");
            htmlSb.AppendLine("<img src=\"{1}\" />");
            htmlSb.AppendLine("<br />");
            htmlSb.AppendLine("<hr />");
            htmlSb.AppendLine("<ul>");
            htmlSb.AppendLine("<li><strong>Person Name: </strong>{2}</li>");
            htmlSb.AppendLine("<li><strong>Service: </strong>{3}</li>");
            htmlSb.AppendLine("<li><strong>Duration: </strong>{4}</li>");
            htmlSb.AppendLine("<li><strong>Date and Time: </strong>{5}</li>");
            htmlSb.AppendLine("<ul>");
            htmlSb.AppendLine("<br />");
            htmlSb.AppendLine("<h3>We wish you a pleasant stay in our salon!</h3>");

            return htmlSb.ToString().TrimEnd();
        }
    }
}
