namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminProductCategoriesService
    {
        Task<int> CreateAsync(string name);

        Task<int> GetCategoryIdByName(string name);

        Task<IEnumerable<T>> GetCategoriesList<T>();

        Task<IEnumerable<string>> GetCategoryNames();

        Task<bool> Edit(int? categoryId, string name);

        Task<bool> Delete(int? categoryId);

        Task<bool> Undelete(int? categoryId);

        Task<T> GetEditModel<T>(int? id);
    }
}
