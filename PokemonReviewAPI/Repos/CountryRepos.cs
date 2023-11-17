using Microsoft.EntityFrameworkCore;
using PokemonReviewAPI.Data;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Repos {
    public class CountryRepos : ICountryRepos {
        private readonly AppDbContext _dbContext;
        public CountryRepos(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<bool> CountryExists(int id) {
            return await _dbContext.Countries.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Country>> GetCountries() {
            return await _dbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetCountry(int id) {
            return await _dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Country> GetCountryByOwner(int id) {
            return await _dbContext.Owners.Where(x => x.Id == id).Select(x => x.Country).FirstOrDefaultAsync();
        }

        public async Task<List<Owner>> GetOwnersFromACountry(int id) {
            return await _dbContext.Owners.Where(x => x.Country.Id == id).ToListAsync();
        }
    }
}
