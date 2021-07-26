namespace SenseIt.Web.ViewModels.Admin.Users
{
    using System.ComponentModel.DataAnnotations;

    public class AddUserToRolePostModel
    {
        [Required]
        public string Role { get; set; }
    }
}
