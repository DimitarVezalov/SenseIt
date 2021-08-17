namespace SenseIt.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }

        [Required]
        [MaxLength(5)]
        public string Number { get; set; }

        [Required]
        [MaxLength(15)]
        public string ZipCode { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }
    }
}
