namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SenseIt.Services.Data.Admin.Models.Categories;

    public interface IAdminServiceCategoriesService
    {
        Task<int> CreateAsync(string name);

        Task<int> GetCategoryIdByName(string name);

        Task<IEnumerable<CategoriesListingViewModel>> GetCategoriesList();

        Task<IEnumerable<string>> GetCategoryNames();

        Task<bool> Edit(int? categoryId, string name);

        Task<bool> Delete(int? categoryId);

        Task<bool> Undelete(int? categoryId);
    }
}
