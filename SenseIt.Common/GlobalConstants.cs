namespace SenseIt.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Sense It";
        public const string MissingCategoryValue = "Undefined";

        public static class ProductConstants
        {
            public const int ProductNameMinLength = 5;
            public const int ProductNameMaxLength = 100;
            public const int ProductDescriptionMinLength = 5;
            public const int ProductDescriptionMaxLength = 800;
            public const int ProductRestockQuantity = 1000;
            public const int ProductBrandMinLength = 2;
            public const int ProductBrandMaxLength = 30;

            public const string ProductNotAvailableString = "Out Of Stock";
            public const string ProductAvailableString = "In Stock";
        }

        public static class ServiceConstants
        {
            public const int ServiceNameMinLength = 5;
            public const int ServiceNameMaxLength = 100;
            public const int ServiceDescriptionMinLength = 5;
            public const int ServiceDescriptionMaxLength = 800;

            public const string ServiceDurationRegex = @"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$";
            public const string ServiceDurationExMessagge = "Invalid time format and hh:mm values.";
        }

        public static class CategoryConstants
        {
            public const int CategoryNameMinLength = 5;
            public const int CategoryNameMaxLength = 50;
        }

        public static class Role
        {
            public const string AdministratorRoleName = "Administrator";
            public const string UserRoleName = "User";
            public const string EmployeeRoleName = "Employee";
        }

        public static class Admin
        {
            public const string AdminEmail = "Admin1@gmail.com";
            public const string AdminPassword = "Admin1";
            public const string AdminUsername = "Admin";

            public const string AdminDashboardIndex = "/Administration/Dashboard/Index";
        }

        public static class Account
        {
            public const string LockedAccount = "Locked";
            public const string UnLockedAccount = "Unlocked";
        }

        public static class Pagination
        {
            public const int ProductsPerPage = 9;
            public const int DefaultStartingPage = 1;
        }
    }
}
