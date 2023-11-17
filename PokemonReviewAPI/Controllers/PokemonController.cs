using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase {
        private readonly IPokemonRepos _pokemonRepos;
        private readonly ICategoryRepos _categoryRepos;
        public PokemonController(IPokemonRepos pokemonRepos, ICategoryRepos categoryRepos) {
            _pokemonRepos= pokemonRepos;
            _categoryRepos=categoryRepos;
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

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<Pokemon>>> GetPokemonsByCategory(int categoryId) {
            if(!await _categoryRepos.CategoryExists(categoryId)) return NotFound();
            var pokemons = await _pokemonRepos.GetPokemonByCategory(categoryId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

    }
}
