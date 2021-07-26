namespace SenseIt.Web.ViewModels.Admin.Users
{
    using System.Collections.Generic;

    using AutoMapper;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class AdminUsersListingModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }

        public bool IsLocked { get; set; }
    }
}
