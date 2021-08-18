namespace SenseIt.Web.ViewModels.Payments
{
    using System.ComponentModel.DataAnnotations;

    public class PaymentDetailsFormModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Street { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Town { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(15)]
        public string Number { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string ZipCode { get; set; }
    }
}
