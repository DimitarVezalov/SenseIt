namespace SenseIt.Web.Utility
{
    using System.Text;

    public static class EmailSenderHelper
    {
        public static string PrepareAppointmentHtml()
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

        public static string PrepareOrderHtml()
        {
            var htmlSb = new StringBuilder();
            htmlSb.AppendLine("<h1 class=\"text-center text-danger\">Dear {0}</h1>");
            htmlSb.AppendLine("<br />");
            htmlSb.AppendLine("<h2>Thank You For Choosing Sense It Beauty Studio</h2>");
            htmlSb.AppendLine("<hr />");
            htmlSb.AppendLine("<h3>Your Order Id: {1}, Order Status: <span style\"color: red\">Pending</span></h3>");
            htmlSb.AppendLine("<br />");
            htmlSb.AppendLine("<hr />");
            htmlSb.AppendLine("<h3>Products:</h3>");
            htmlSb.AppendLine("<div> {2} </div>");
            htmlSb.AppendLine("<br />");
            htmlSb.AppendLine("<hr />");
            htmlSb.AppendLine("<div>Total Sum: ${3}</div>");
            htmlSb.AppendLine("<br />");
            htmlSb.AppendLine("<hr />");
            htmlSb.AppendLine("<h3>Date: {4}</h3>");

            return htmlSb.ToString().TrimEnd();
        }
    }
}
