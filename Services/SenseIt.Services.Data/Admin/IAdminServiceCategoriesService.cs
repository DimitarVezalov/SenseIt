namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SenseIt.Services.Data.Admin.Models.Categories;
    using SenseIt.Services.Data.Admin.Models.Categories.AppServiceCategory;

    public interface IAdminServiceCategoriesService
    {
        Task<int> CreateAsync(string name, string description, string imageUrl);

        Task<int> GetCategoryIdByName(string name);

        Task<AppServiceCategoryAddEditModel> GetEditModel(int? id);

        Task<IEnumerable<CategoriesListingViewModel>> GetCategoriesList();

        Task<IEnumerable<string>> GetCategoryNames();

        Task<bool> Edit(int? categoryId, string name, string description, string imageUrl);

        Task<bool> Delete(int? categoryId);

        Task<bool> Undelete(int? categoryId);
    }
}
