using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface IReviewRepos {
        Task<List<Review>> GetReviews();
        Task<Review> GetReview(int id);
        Task<List<Review>> GetReviewsOfAPokemon(int id);
        Task<bool> ReviewExists(int id);
        Task<Review> CreateReview(Review review);
        Review ConvertFromDto(ReviewDto reviewDto);
    }
}
