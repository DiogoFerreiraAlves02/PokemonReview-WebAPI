using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface IOwnerRepos {
        Task<List<Owner>> GetOwners();
        Task<Owner> GetOwner(int id);
        Task<List<Owner>> GetOwnersOfAPokemon(int id);
        Task<List<Pokemon>> GetPokemonsByOwner(int id);
        Task<bool> OwnerExists(int id);
        Task<Owner> CreateOwner(Owner owner);
        Task<Owner> UpdateOwner(Owner owner);
        Owner ConvertFromDto(OwnerDto ownerDto);
        Task<bool> DeleteOwner(Owner owner);
    }
}
