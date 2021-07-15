using System.Collections.Generic;

namespace SenseIt.Services.Data.Admin.Models.Users
{
    public class AdminUsersListingModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }

        public bool IsLocked { get; set; }
    }
}
