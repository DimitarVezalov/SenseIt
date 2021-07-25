namespace SenseIt.Services.Data.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static SenseIt.Common.GlobalConstants.CategoryConstants;

    public class CategoryAddEditModel
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
