namespace SenseIt.Web.ViewModels.Admin.Categories
{
    using System.ComponentModel.DataAnnotations;

    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    using static SenseIt.Common.GlobalConstants.CategoryConstants;

    public class ProductCategoryAddEditModel : IMapFrom<ProductCategory>
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
