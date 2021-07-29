namespace SenseIt.Services.Data
{
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<int> CreateAsync(int appServiceId, string postedById, string content, double rating);
    }
}
