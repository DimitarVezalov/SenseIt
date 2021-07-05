namespace SenseIt.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SenseIt.Services.Data.Admin.Models;

    public interface IAdminProductsService
    {
        Task CreateAsync(CreateProductInputModel model);

        Task<IEnumerable<AdminProductsListingViewModel>> GetAllProductsAsync();
    }
}
