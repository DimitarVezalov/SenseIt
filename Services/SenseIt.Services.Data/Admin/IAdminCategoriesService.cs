namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminCategoriesService
    {
        Task<int> GetCategoryIdByName(string name);

        Task<IEnumerable<string>> GetCategoryNames();
    }
}
