namespace SenseIt.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;

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
    }
}
