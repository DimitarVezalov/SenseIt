namespace SenseIt.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    public class ContactEmailFormModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Subject { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(2000)]
        public string Message { get; set; }
    }
}
