namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminServiceCategoriesService
    {
        Task<int> CreateAsync(string name, string description, string imageUrl);

        Task<int> GetCategoryIdByName(string name);

        Task<T> GetEditModel<T>(int? id);

        Task<IEnumerable<T>> GetCategoriesList<T>();

        Task<IEnumerable<string>> GetCategoryNames();

        Task<bool> Edit(int? categoryId, string name, string description, string imageUrl);

        Task<bool> Delete(int? categoryId);

        Task<bool> Undelete(int? categoryId);
    }
}
