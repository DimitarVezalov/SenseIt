namespace SenseIt.Services.Data
{
    using System.Threading.Tasks;

    public interface ITownsService
    {
        Task<int> GetTownId(string name);
    }
}
