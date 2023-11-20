using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Repos {
    public class ReviewRepos : IReviewRepos {
        private readonly AppDbContext _dbContext;
        public ReviewRepos(AppDbContext dbContext) {
            _dbContext=dbContext;
        }

        public async Task<bool> ReviewExists(int id) {
            return await _dbContext.Reviews.AnyAsync(x => x.Id == id);
        }

        public async Task<Review> GetReview(int id) {
            return await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Review>> GetReviews() {
            return await _dbContext.Reviews.ToListAsync();
        }

        public async Task<List<Review>> GetReviewsOfAPokemon(int id) {
            return await _dbContext.Reviews.Where(x => x.Pokemon.Id == id).ToListAsync();
        }

        public async Task<Review> CreateReview(Review review) {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public Review ConvertFromDto(ReviewDto reviewDto) {
            return new Review { Id= reviewDto.Id, Title=reviewDto.Title, Text = reviewDto.Text, Rating = reviewDto.Rating};
        }

        public async Task<Review> UpdateReview(Review review) {
            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
    }
}
