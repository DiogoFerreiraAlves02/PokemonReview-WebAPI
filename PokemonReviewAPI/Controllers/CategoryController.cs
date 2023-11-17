using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Models;
using PokemonReviewAPI.Repos;
using PokemonReviewAPI.Repos.Interfaces;

namespace PokemonReviewAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase {
        private readonly ICategoryRepos _categoryRepos;
        public CategoryController(ICategoryRepos categoryRepos) {
            _categoryRepos= categoryRepos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories() {
            var categories = await _categoryRepos.GetCategories();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Category>> GetCategory(int categoryId) {
            if (!await _categoryRepos.CategoryExists(categoryId)) return NotFound();
            var category = await _categoryRepos.GetCategory(categoryId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

    }
}
