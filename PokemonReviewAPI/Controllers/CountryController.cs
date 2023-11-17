﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase {
        private readonly ICountryRepos _countryRepos;
        private readonly IOwnerRepos _ownerRepos;
        public CountryController(ICountryRepos countryRepos, IOwnerRepos ownerRepos) {
            _countryRepos = countryRepos;
            _ownerRepos=ownerRepos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries() {
            var countries = await _countryRepos.GetCountries();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<Country>> GetCountry(int countryId) {
            if (!await _countryRepos.CountryExists(countryId)) return NotFound();
            var country = await _countryRepos.GetCountry(countryId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        public async Task<ActionResult<Country>> GetCountryByOwner(int ownerId) {
            if (!await _ownerRepos.OwnerExists(ownerId)) return NotFound();
            var country = await _countryRepos.GetCountryByOwner(ownerId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry([FromBody] CountryDto countryCreate) {
            if (countryCreate == null) return BadRequest(ModelState);

            Country country = _countryRepos.ConvertFromDto(countryCreate);

            if (await _countryRepos.CheckDuplicateCountry(country) != null) {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            country = await _countryRepos.CreateCountry(country);

            return Ok(country);
        }

    }
}
