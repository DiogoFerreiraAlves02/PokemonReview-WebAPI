using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface IOwnerRepos {
        Task<List<Owner>> GetOwners();
        Task<Owner> GetOwner(int id);
        Task<List<Owner>> GetOwnersOfAPokemon(int id);
        Task<List<Pokemon>> GetPokemonsByOwner(int id);
        Task<bool> OwnerExists(int id);
    }
}
