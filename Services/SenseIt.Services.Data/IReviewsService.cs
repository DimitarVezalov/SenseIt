namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<int> CreateAsync(int appServiceId, string postedById, string content, double rating);

        Task<IEnumerable<T>> GetAllByAppService<T>(int? appServiceId);
    }
}
