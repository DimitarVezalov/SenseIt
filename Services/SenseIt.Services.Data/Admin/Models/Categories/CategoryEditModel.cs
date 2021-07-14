namespace SenseIt.Services.Data.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static SenseIt.Common.GlobalConstants.CategoryConstants;

    public class CategoryEditModel
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
