using System.Text.Json.Serialization;

namespace PokemonReviewAPI.Models {
    public class Pokemon {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        [JsonIgnore]
        public List<Review> Reviews { get; set; }
        [JsonIgnore]
        public List<PokemonOwner> PokemonOwners { get; set; }
        [JsonIgnore]
        public List<PokemonCategory> PokemonCategories { get; set; }
    }
}
