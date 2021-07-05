using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenseIt.Services.Data.Admin
{
    public interface IAdminCategoriesService
    {
        int GetCategoryIdByName(string name);

        Task<IEnumerable<string>> GetCategoryNames();
    }
}
