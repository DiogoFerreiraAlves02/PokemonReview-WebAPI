using PokemonReviewAPI.Dto;
using PokemonReviewAPI.Models;

namespace PokemonReviewAPI.Repos.Interfaces {
    public interface IReviewerRepos {
        Task<List<Reviewer>> GetReviewers();
        Task<Reviewer> GetReviewer(int id);
        Task<List<Review>> GetReviewsOfAReviewer(int id);
        Task<bool> ReviewerExists(int id);
        Task<Reviewer> CreateReviewer(Reviewer reviewer);
        Reviewer ConvertFromDto(ReviewerDto reviewerDto);
        Task<Reviewer> UpdateReviewer(Reviewer reviewer);
    }
}
