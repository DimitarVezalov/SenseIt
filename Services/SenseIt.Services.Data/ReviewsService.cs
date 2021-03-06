namespace SenseIt.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Services.Mapping;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<int> CreateAsync(int? appServiceId, string postedById, string content, int rating)
        {
            var review = new Review
            {
                AppServiceId = (int)appServiceId,
                PostedById = postedById,
                Content = content,
                Rating = rating,
            };

            await this.reviewRepository.AddAsync(review);
            var result = await this.reviewRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var review = await this.reviewRepository
                .All()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return false;
            }

            this.reviewRepository.HardDelete(review);
            var result = await this.reviewRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<T>> GetAllByAppService<T>(int? appServiceId)
        {
            if (appServiceId == null)
            {
                return null;
            }

            var reviews = await this.reviewRepository
                .AllAsNoTracking()
                .Where(r => r.AppServiceId == appServiceId)
                .OrderByDescending(r => r.CreatedOn)
                .To<T>()
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<T>> GetAllByUserId<T>(string userId)
        {
            var reviews = await this.reviewRepository
                .AllAsNoTracking()
                .Where(r => r.PostedById == userId)
                .OrderByDescending(r => r.CreatedOn)
                .To<T>()
                .ToListAsync();

            return reviews;
        }
    }
}
