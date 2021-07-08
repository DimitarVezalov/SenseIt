namespace SenseIt.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SenseIt.Data.Common.Models;

    public class ServiceCategory : BaseDeletableModel<int>
    {
        public ServiceCategory()
        {
            this.Services = new HashSet<Service>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
