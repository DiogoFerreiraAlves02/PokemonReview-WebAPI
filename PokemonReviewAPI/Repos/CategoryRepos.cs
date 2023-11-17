using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Dto;
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

        public async Task<List<Pokemon>> GetPokemonByCategory(int id) {
            return await _dbContext.PokemonCategories.Where(x => x.CategoryId == id).Select(x => x.Pokemon).ToListAsync();
        }

        public async Task<Category> CreateCategory(Category category) {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> CheckDuplicateCategory(Category category) {
            return await _dbContext.Categories.Where(x => x.Name.Trim().ToUpper() == category.Name.Trim().ToUpper()).FirstOrDefaultAsync();
        }

        public Category ConvertFromDto(CategoryDto categoryDto) {
            return new Category { Id = categoryDto.Id, Name = categoryDto.Name };
        }
    }
}
