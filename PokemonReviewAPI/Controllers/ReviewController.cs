using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase {
        private readonly IReviewRepos _reviewRepos;
        private readonly IPokemonRepos _pokemonRepos;
        public ReviewController(IReviewRepos reviewRepos, IPokemonRepos pokemonRepos) {
            _reviewRepos=reviewRepos;
            _pokemonRepos=pokemonRepos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetReviews() {
            var reviews = await _reviewRepos.GetReviews();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<Review>> GetReview(int reviewId) {
            if (!await _reviewRepos.ReviewExists(reviewId)) return NotFound();
            var review = await _reviewRepos.GetReview(reviewId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        [HttpGet("pokemon/{pokemonId}")]
        public async Task<ActionResult<List<Review>>> GetReviewsOfAPokemon(int pokemonId) {
            if (!await _pokemonRepos.PokemonExists(pokemonId)) return NotFound();
            var reviews = await _reviewRepos.GetReviewsOfAPokemon(pokemonId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

    }
}
