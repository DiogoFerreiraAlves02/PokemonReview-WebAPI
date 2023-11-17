using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Repos {
    public class OwnerRepos : IOwnerRepos {
        private readonly AppDbContext _dbContext;
        public OwnerRepos(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Owner> GetOwner(int id) {
            return await _dbContext.Owners.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Owner>> GetOwners() {
            return await _dbContext.Owners.ToListAsync();
        }

        public async Task<List<Owner>> GetOwnersOfAPokemon(int id) {
            return await _dbContext.PokemonOwners.Where(x => x.Pokemon.Id == id).Select(x => x.Owner).ToListAsync();
        }

        public async Task<List<Pokemon>> GetPokemonsByOwner(int id) {
            return await _dbContext.PokemonOwners.Where(x => x.Owner.Id == id).Select(x => x.Pokemon).ToListAsync();
        }

        public async Task<bool> OwnerExists(int id) {
            return await _dbContext.Owners.AnyAsync(x => x.Id == id);
        }

        public async Task<Owner> CreateOwner(Owner owner) {
            await _dbContext.Owners.AddAsync(owner);
            await _dbContext.SaveChangesAsync();
            return owner;
        }

        public Owner ConvertFromDto(OwnerDto ownerDto) {
            return new Owner { Id = ownerDto.Id, FirstName = ownerDto.FirstName, LastName = ownerDto.LastName, Gym = ownerDto.Gym };
        }
    }
}
