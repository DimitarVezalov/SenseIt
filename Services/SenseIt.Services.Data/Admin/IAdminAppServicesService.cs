using SenseIt.Services.Data.Admin.Models.AppServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenseIt.Services.Data.Admin
{
    public interface IAdminAppServicesService
    {
        Task<int> CreateAsync(CreateServiceInputModel model);

        Task<IEnumerable<AdminServiceListingViewModel>> GetAllServicesAsync();

        //Task<bool> Delete(int? productId);

        //Task<AdminProductDetailsViewModel> GetDetailsModel(int? id);

        //Task<bool> Update(int? id, AdminProductUpdateModel model);
    }
}
