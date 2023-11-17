using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Repos {
    public class CategoryRepos : ICategoryRepos {
        private readonly AppDbContext _dbContext;
        public CategoryRepos(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<bool> CategoryExists(int id) {
            return await _dbContext.Categories.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetCategories() {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id) {
            return await _dbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
