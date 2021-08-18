namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductCategoriesService
    {
        Task<IEnumerable<string>> GetProductCategories();
    }
}
