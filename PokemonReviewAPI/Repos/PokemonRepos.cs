using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Dto;
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

        public async Task<Pokemon> CreatePokemon(int ownerId, int categoryId, Pokemon pokemon) {
            var pokemonOwnerEntity = await _dbContext.Owners.Where(x => x.Id == ownerId).FirstOrDefaultAsync();
            var pokemonCategoryEntity = await _dbContext.Categories.Where(x => x.Id == categoryId).FirstOrDefaultAsync();

            var pokemonOwner = new PokemonOwner() {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            await _dbContext.PokemonOwners.AddAsync(pokemonOwner);

            var pokemonCategory = new PokemonCategory() {
                Category = pokemonCategoryEntity,
                Pokemon = pokemon
            };

            await _dbContext.PokemonCategories.AddAsync(pokemonCategory);
            await _dbContext.Pokemons.AddAsync(pokemon);

            await _dbContext.SaveChangesAsync();

            return pokemon;
        }

        public async Task<Pokemon> CheckDuplicatePokemon(Pokemon pokemon) {
            return await _dbContext.Pokemons.Where(x => x.Name.Trim().ToUpper() == pokemon.Name.Trim().ToUpper()).FirstOrDefaultAsync();
        }

        public Pokemon ConvertFromDto(PokemonDto pokemonDto) {
            return new Pokemon { Id = pokemonDto.Id, Name = pokemonDto.Name, BirthDate = pokemonDto.BirthDate};
        }
    }
}
