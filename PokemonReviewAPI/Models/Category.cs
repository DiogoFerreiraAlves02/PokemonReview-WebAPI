using System.Text.Json.Serialization;

namespace PokemonReviewAPI.Models {
    public class Category {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<PokemonCategory> PokemonCategories { get; set; }
    }
}
