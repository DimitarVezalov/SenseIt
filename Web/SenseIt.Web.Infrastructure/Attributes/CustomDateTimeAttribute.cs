namespace SenseIt.Web.Infrastructure.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomDateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            if (DateTime.Now.CompareTo(value) <= 0 && DateTime.Now.AddYears(1).CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date and Time Must Be After Current Date Time!");
            }
        }
    }
}
