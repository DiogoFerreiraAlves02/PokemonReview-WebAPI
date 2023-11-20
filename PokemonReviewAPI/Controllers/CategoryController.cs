using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewAPI.Dto;
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

        [HttpGet("{categoryId}/pokemons")]
        public async Task<ActionResult<List<Pokemon>>> GetPokemonsByCategory(int categoryId) {
            if (!await _categoryRepos.CategoryExists(categoryId)) return NotFound();
            var pokemons = await _categoryRepos.GetPokemonByCategory(categoryId);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CategoryDto categoryCreate) {
            if (categoryCreate == null) return BadRequest(ModelState);

            Category category = _categoryRepos.ConvertFromDto(categoryCreate);

            if (await _categoryRepos.CheckDuplicateCategory(category) != null) {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            category = await _categoryRepos.CreateCategory(category);

            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult<Category>> UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory) {
            if (updatedCategory == null) return BadRequest(ModelState);

            if(categoryId != updatedCategory.Id) return BadRequest(ModelState);

            if(!await _categoryRepos.CategoryExists(categoryId)) return NotFound();

            Category category = _categoryRepos.ConvertFromDto(updatedCategory);

            await _categoryRepos.UpdateCategory(category);

            return Ok(category);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<bool>> DeleteCategory(int categoryId) {
            if (!await _categoryRepos.CategoryExists(categoryId)) return NotFound();

            var categoryToDelete = await _categoryRepos.GetCategory(categoryId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var deleted = await _categoryRepos.DeleteCategory(categoryToDelete);

            if (!deleted) {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }
            return Ok(deleted);
        }
    }
}
