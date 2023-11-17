using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase {
        private readonly IReviewerRepos _reviewerRepos;
        public ReviewerController(IReviewerRepos reviewerRepos) {
            _reviewerRepos=reviewerRepos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reviewer>>> GetReviewers() {
            var reviewers = await _reviewerRepos.GetReviewers();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        public async Task<ActionResult<Reviewer>> GetReviewer(int reviewerId) {
            if (!await _reviewerRepos.ReviewerExists(reviewerId)) return NotFound();
            var reviewer = await _reviewerRepos.GetReviewer(reviewerId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        public async Task<ActionResult<List<Review>>> GetReviewsOfAReviewer(int reviewerId) {
            if (!await _reviewerRepos.ReviewerExists(reviewerId)) return NotFound();
            var reviews = await _reviewerRepos.GetReviewsOfAReviewer(reviewerId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

    }
}
