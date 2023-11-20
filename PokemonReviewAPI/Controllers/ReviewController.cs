using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase {
        private readonly IReviewRepos _reviewRepos;
        private readonly IPokemonRepos _pokemonRepos;
        private readonly IReviewerRepos _reviewerRepos;
        public ReviewController(IReviewRepos reviewRepos, IPokemonRepos pokemonRepos, IReviewerRepos reviewerRepos) {
            _reviewRepos=reviewRepos;
            _pokemonRepos=pokemonRepos;
            _reviewerRepos=reviewerRepos;
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

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDto reviewCreate) {
            if (reviewCreate == null) return BadRequest(ModelState);

            Review review = _reviewRepos.ConvertFromDto(reviewCreate);
            review.Pokemon = await _pokemonRepos.GetPokemon(pokemonId);
            review.Reviewer = await _reviewerRepos.GetReviewer(reviewerId);
            review = await _reviewRepos.CreateReview(review);

            return Ok(review);
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult<Review>> UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview) {
            if (updatedReview == null) return BadRequest(ModelState);

            if (reviewId != updatedReview.Id) return BadRequest(ModelState);

            if (!await _reviewRepos.ReviewExists(reviewId)) return NotFound();

            Review review = _reviewRepos.ConvertFromDto(updatedReview);

            await _reviewRepos.UpdateReview(review);

            return Ok(review);
        }

    }
}
