using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase {
        private readonly IPokemonRepos _pokemonRepos;
        private readonly IReviewRepos _reviewRepos;

        public PokemonController(IPokemonRepos pokemonRepos, IReviewRepos reviewRepos) {
            _pokemonRepos= pokemonRepos;
            _reviewRepos= reviewRepos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetPokemons() {
            var pokemons = await _pokemonRepos.GetPokemons();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpGet("{pokemonId}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(int pokemonId) {
            if (!await _pokemonRepos.PokemonExists(pokemonId)) return NotFound();
            var pokemon = await _pokemonRepos.GetPokemon(pokemonId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }

        [HttpGet("{pokemonId}/rating")]
        public async Task<ActionResult<decimal>> GetPokemonRating(int pokemonId) {
            if (!await _pokemonRepos.PokemonExists(pokemonId)) return NotFound();
            var rating = await _pokemonRepos.GetPokemonRating(pokemonId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }

        [HttpPost]
        public async Task<ActionResult<Pokemon>> CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate) {
            if (pokemonCreate == null) return BadRequest(ModelState);

            Pokemon pokemon = _pokemonRepos.ConvertFromDto(pokemonCreate);

            if (await _pokemonRepos.CheckDuplicatePokemon(pokemon) != null) {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            pokemon = await _pokemonRepos.CreatePokemon(ownerId, categoryId, pokemon);

            return Ok(pokemon);
        }

        [HttpPut("{pokemonId}")]
        public async Task<ActionResult<Pokemon>> UpdatePokemon(int pokemonId,[FromBody] PokemonDto updatedPokemon) {
            if (updatedPokemon == null) return BadRequest(ModelState);

            if (pokemonId != updatedPokemon.Id) return BadRequest(ModelState);

            if (!await _pokemonRepos.PokemonExists(pokemonId)) return NotFound();

            Pokemon pokemon = _pokemonRepos.ConvertFromDto(updatedPokemon);

            await _pokemonRepos.UpdatePokemon(pokemon);

            return Ok(pokemon);
        }

    }
}
