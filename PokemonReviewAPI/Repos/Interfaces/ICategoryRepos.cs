using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface ICategoryRepos {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<List<Pokemon>> GetPokemonByCategory(int id);
        Task<bool> CategoryExists(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> CheckDuplicateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Category ConvertFromDto(CategoryDto categoryDto);
        Task<bool> DeleteCategory(Category category);
    }
}
