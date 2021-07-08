namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SenseIt.Services.Data.Admin.Models.Categories;

    public interface IAdminProductCategoriesService
    {
        Task<int> GetCategoryIdByName(string name);

        Task<IEnumerable<ProductCategoriesListingViewModel>> GetCategoriesList();

        Task<IEnumerable<string>> GetCategoryNames();

        Task<bool> Edit(int? categoryId, ProductCategoryEditModel model);
    }
}
