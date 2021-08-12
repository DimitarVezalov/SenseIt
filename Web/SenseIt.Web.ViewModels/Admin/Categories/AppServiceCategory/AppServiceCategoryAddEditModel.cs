namespace SenseIt.Web.ViewModels.Admin.Categories.AppServiceCategory
{
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    using static SenseIt.Common.GlobalConstants;

    public class AppServiceCategoryAddEditModel : IMapFrom<ServiceCategory>
    {
        [Required]
        [MinLength(CategoryConstants.CategoryNameMinLength)]
        [MaxLength(CategoryConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(CategoryConstants.CategoryDescriptionMinLength)]
        [MaxLength(CategoryConstants.CategoryDescriptionMaxLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
