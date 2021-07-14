namespace SenseIt.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SenseIt.Data.Common.Models;

    using static SenseIt.Common.GlobalConstants.CategoryConstants;

    public class ServiceCategory : BaseDeletableModel<int>
    {
        public ServiceCategory()
        {
            this.Services = new HashSet<Service>();
        }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
