using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Repos {
    public class PokemonRepos : IPokemonRepos{
        private readonly AppDbContext _dbContext;
        public PokemonRepos(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<List<Pokemon>> GetPokemons() {
            return await _dbContext.Pokemons.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Pokemon> GetPokemon(int id) {
            return await _dbContext.Pokemons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pokemon> GetPokemon(string name) {
            return await _dbContext.Pokemons.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<decimal> GetPokemonRating(int id) {
            var review = _dbContext.Reviews.Where(x => x.Pokemon.Id == id);
            if(review.Count() <= 0) {
                return 0;
            }
            return (decimal) review.Sum(x => x.Rating) / review.Count();
        }

        public async Task<bool> PokemonExists(int id) {
            return await _dbContext.Pokemons.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Pokemon>> GetPokemonByCategory(int id) {
            return await _dbContext.PokemonCategories.Where(x => x.CategoryId == id).Select(x => x.Pokemon).ToListAsync();
        }
    }
}
