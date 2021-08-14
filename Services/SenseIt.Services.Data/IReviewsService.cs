namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<int> CreateAsync(int? appServiceId, string postedById, string content, int rating);

        Task<IEnumerable<T>> GetAllByAppService<T>(int? appServiceId);

        Task<IEnumerable<T>> GetAllByUserId<T>(string userId);

        Task<bool> Delete(int id);
    }
}
