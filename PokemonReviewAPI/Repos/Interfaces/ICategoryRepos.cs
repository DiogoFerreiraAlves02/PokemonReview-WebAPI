using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface ICategoryRepos {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<bool> CategoryExists(int id);
    }
}
