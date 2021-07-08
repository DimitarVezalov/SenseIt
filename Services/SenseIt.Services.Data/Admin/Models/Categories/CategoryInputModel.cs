namespace SenseIt.Services.Data.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
