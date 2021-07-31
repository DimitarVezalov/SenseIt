namespace SenseIt.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SenseIt.Data.Common.Models;

    using static SenseIt.Common.GlobalConstants.Review;

    public class Review : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        [ForeignKey(nameof(PostedBy))]
        public string PostedById { get; set; }

        public ApplicationUser PostedBy { get; set; }

        [Required]
        [ForeignKey(nameof(AppService))]
        public int AppServiceId { get; set; }

        public Service AppService { get; set; }

        public int Rating { get; set; }
    }
}
