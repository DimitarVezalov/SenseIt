namespace SenseIt.Services.Data.Admin.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class AddUserToRolePostModel
    {
        [Required]
        public string Role { get; set; }
    }
}
