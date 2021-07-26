namespace SenseIt.Web.ViewModels.Admin.Users
{
    using System.ComponentModel.DataAnnotations;

    public class RemoveUserFromRolePostModel
    {
        [Required]
        public string Role { get; set; }
    }
}
