using SenseIt.Services.Data.Admin.Models.AppServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenseIt.Services.Data.Admin
{
    public interface IAdminAppServicesService
    {
        Task<int> CreateAsync(CreateAppServiceInputModel model);

        Task<IEnumerable<AdminAppServiceListingViewModel>> GetAllServicesAsync();

        Task<bool> Delete(int? serviceId);

        Task<AppServiceDetailsModel> GetDetailsModel(int? id);

        Task<bool> Update(int? id, EditAppServiceInputModel model);

        Task<bool> Undelete(int? serviceId);
    }
}
