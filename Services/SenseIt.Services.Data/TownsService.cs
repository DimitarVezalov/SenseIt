namespace SenseIt.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

    public class TownsService : ITownsService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownsService(IDeletableEntityRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }

        public async Task<int> GetTownId(string name)
        {
            if (!this.townRepository.AllAsNoTracking().Any(t => t.Name == name))
            {
                await this.CreateTown(name);
            }

            var townId = await this.townRepository
                .AllAsNoTracking()
                .Where(t => t.Name == name)
                .Select(t => t.Id)
                .FirstOrDefaultAsync();

            return townId;
        }

        private async Task CreateTown(string name)
        {
            var town = new Town
            {
                Name = name,
            };

            await this.townRepository.AddAsync(town);
            var result = await this.townRepository.SaveChangesAsync();
        }
    }
}
