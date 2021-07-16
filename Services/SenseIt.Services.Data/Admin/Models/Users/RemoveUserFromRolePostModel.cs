namespace SenseIt.Services.Data.Admin.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class RemoveUserFromRolePostModel
    {
        [Required]
        public string Role { get; set; }
    }
}
