using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Dto;
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

        [HttpPost]
        public async Task<ActionResult<Reviewer>> CreateReviewer([FromBody] ReviewerDto reviewerCreate) {
            if (reviewerCreate == null) return BadRequest(ModelState);

            Reviewer reviewer = _reviewerRepos.ConvertFromDto(reviewerCreate);

            reviewer = await _reviewerRepos.CreateReviewer(reviewer);

            return Ok(reviewer);
        }

        [HttpPut("{reviewerId}")]
        public async Task<ActionResult<Reviewer>> UpdateReviewer(int reviewerId, [FromBody] ReviewerDto updatedReviewer) {
            if (updatedReviewer == null) return BadRequest(ModelState);

            if (reviewerId != updatedReviewer.Id) return BadRequest(ModelState);

            if (!await _reviewerRepos.ReviewerExists(reviewerId)) return NotFound();

            Reviewer reviewer = _reviewerRepos.ConvertFromDto(updatedReviewer);

            await _reviewerRepos.UpdateReviewer(reviewer);

            return Ok(reviewer);
        }

        [HttpDelete("{reviewerId}")]
        public async Task<ActionResult<bool>> DeleteReviewer(int reviewerId) {
            if (!await _reviewerRepos.ReviewerExists(reviewerId)) return NotFound();

            var reviewerToDelete = await _reviewerRepos.GetReviewer(reviewerId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var deleted = await _reviewerRepos.DeleteReviewer(reviewerToDelete);

            if (!deleted) {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }
            return Ok(deleted);
        }

    }
}
