namespace SenseIt.Services.Data
{
    using System.Threading.Tasks;

    public interface IAddressService
    {
        Task<int> GetAddressId(string userId ,string town, string street, string number, string zipCode);
    }
}
