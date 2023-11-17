﻿using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface ICountryRepos {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountry(int id);
        Task<Country> GetCountryByOwner(int id);
        Task<List<Owner>> GetOwnersFromACountry(int id);
        Task<bool> CountryExists(int id);

    }
}