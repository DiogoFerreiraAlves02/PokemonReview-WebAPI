using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Repos {
    public class ReviewerRepos : IReviewerRepos{
        private readonly AppDbContext _dbContext;
        public ReviewerRepos(AppDbContext appDbContext) {
            _dbContext=appDbContext;
        }

        public async Task<Reviewer> GetReviewer(int id) {
            return await _dbContext.Reviewers.Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Reviewer>> GetReviewers() {
            return await _dbContext.Reviewers.ToListAsync();
        }

        public async Task<List<Review>> GetReviewsOfAReviewer(int id) {
            return await _dbContext.Reviews.Where(x => x.Reviewer.Id == id).ToListAsync();
        }

        public async Task<bool> ReviewerExists(int id) {
            return await _dbContext.Reviewers.AnyAsync(x => x.Id == id);
        }
    }
}
