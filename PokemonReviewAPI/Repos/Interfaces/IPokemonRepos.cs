using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface IPokemonRepos {
        Task<List<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemon(int id);
        Task<Pokemon> GetPokemon(string name);
        Task<decimal> GetPokemonRating(int id);
        Task<bool> PokemonExists(int id);
        Task<Pokemon> CreatePokemon(int ownerId, int categoryId,Pokemon pokemon);
        Task<Pokemon> CheckDuplicatePokemon(Pokemon pokemon);
        Pokemon ConvertFromDto(PokemonDto pokemonDto);
    }
}
