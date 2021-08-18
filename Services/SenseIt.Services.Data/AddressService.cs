namespace SenseIt.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;
        private readonly ITownsService townsService;

        public AddressService(
            IDeletableEntityRepository<Address> addressRepository,
            ITownsService townsService)
        {
            this.addressRepository = addressRepository;
            this.townsService = townsService;
        }

        public async Task<int> GetAddressId(string userId, string town, string street, string number, string zipCode)
        {
            var townId = await this.townsService.GetTownId(town);

            var isInDatabase = this.addressRepository.AllAsNoTracking()
                .Any(a => a.UserId == userId && a.Street == street && a.Number == number && a.ZipCode == zipCode);

            if (!isInDatabase)
            {
                await this.CreateAddress(userId, townId, street, number, zipCode);
            }

            var addressId = await this.addressRepository
                .AllAsNoTracking()
                .Where(a => a.Town.Name == town && a.Street == street && a.Number == number)
                .Select(a => a.Id)
                .FirstOrDefaultAsync();

            return addressId;
        }

        private async Task CreateAddress(string userId, int townId, string street, string number, string zipCode)
        {
            var address = new Address
            {
                TownId = townId,
                Street = street,
                Number = number,
                ZipCode = zipCode,
                UserId = userId,
            };

            await this.addressRepository.AddAsync(address);
            var result = await this.addressRepository.SaveChangesAsync();
        }
    }
}
