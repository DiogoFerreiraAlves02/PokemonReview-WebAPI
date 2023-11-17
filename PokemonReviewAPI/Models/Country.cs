using System.Text.Json.Serialization;

namespace PokemonReviewAPI.Models {
    public class Country {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Owner> Owners { get; set; }
    }
}
