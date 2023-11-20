using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase {
        private readonly IOwnerRepos _ownerRepos;
        private readonly IPokemonRepos _pokemonRepos;
        private readonly ICountryRepos _countryRepos;
        public OwnerController(IOwnerRepos ownerRepos, IPokemonRepos pokemonRepos, ICountryRepos countryRepos) {
            _ownerRepos=ownerRepos;
            _pokemonRepos=pokemonRepos;
            _countryRepos=countryRepos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Owner>>> GetOwners() {
            var owners = await _ownerRepos.GetOwners();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        public async Task<ActionResult<Owner>> GetOwner(int ownerId) {
            if (!await _ownerRepos.OwnerExists(ownerId)) return NotFound();
            var owner = await _ownerRepos.GetOwner(ownerId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemons")]
        public async Task<ActionResult<List<Pokemon>>> GetPokemonsByOwner(int ownerId) {
            if (!await _ownerRepos.OwnerExists(ownerId)) return NotFound();
            var owner = await _ownerRepos.GetPokemonsByOwner(ownerId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("pokemon/{pokemonId}")]
        public async Task<ActionResult<List<Owner>>> GetOwnersOfAPokemon(int pokemonId) {
            if (!await _pokemonRepos.PokemonExists(pokemonId)) return NotFound();
            var owners = await _ownerRepos.GetOwnersOfAPokemon(pokemonId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }

        [HttpPost]
        public async Task<ActionResult<Owner>> CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate) {
            if (ownerCreate == null) return BadRequest(ModelState);

            Owner owner = _ownerRepos.ConvertFromDto(ownerCreate);
            owner.Country = await _countryRepos.GetCountry(countryId);

            owner = await _ownerRepos.CreateOwner(owner);

            return Ok(owner);
        }

        [HttpPut("{ownerId}")]
        public async Task<ActionResult<Owner>> UpdateOwner(int ownerId, [FromBody] OwnerDto updatedOwner) {
            if (updatedOwner == null) return BadRequest(ModelState);

            if (ownerId != updatedOwner.Id) return BadRequest(ModelState);

            if (!await _ownerRepos.OwnerExists(ownerId)) return NotFound();

            Owner owner = _ownerRepos.ConvertFromDto(updatedOwner);

            await _ownerRepos.UpdateOwner(owner);

            return Ok(owner);
        }

    }
}
