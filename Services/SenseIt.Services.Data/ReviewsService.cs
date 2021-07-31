namespace SenseIt.Services.Data
{
    using System;
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

        public async Task<int> CreateAsync(int appServiceId, string postedById, string content, double rating)
        {
            var review = new Review
            {
                AppServiceId = appServiceId,
                PostedById = postedById,
                Content = content,
                Rating = rating,
            };

            await this.reviewRepository.AddAsync(review);
            var result = await this.reviewRepository.SaveChangesAsync();

            return result;
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
                .To<T>()
                .ToListAsync();

            return reviews;
        }
    }
}
