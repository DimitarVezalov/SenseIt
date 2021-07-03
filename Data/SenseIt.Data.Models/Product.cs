namespace SenseIt.Data.Models
{
    using SenseIt.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
