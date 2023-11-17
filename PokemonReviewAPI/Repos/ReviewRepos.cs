using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
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
    }
}
