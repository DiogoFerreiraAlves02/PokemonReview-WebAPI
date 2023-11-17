using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase {
        private readonly IOwnerRepos _ownerRepos;
        private readonly IPokemonRepos _pokemonRepos;
        public OwnerController(IOwnerRepos ownerRepos, IPokemonRepos pokemonRepos) {
            _ownerRepos=ownerRepos;
            _pokemonRepos=pokemonRepos;
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

    }
}
