using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface IReviewerRepos {
        Task<List<Reviewer>> GetReviewers();
        Task<Reviewer> GetReviewer(int id);
        Task<List<Review>> GetReviewsOfAReviewer(int id);
        Task<bool> ReviewerExists(int id);
    }
}
