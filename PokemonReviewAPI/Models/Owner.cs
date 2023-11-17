using System.Text.Json.Serialization;

namespace PokemonReviewAPI.Models {
    public class Owner {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
        [JsonIgnore]
        public List<PokemonOwner> PokemonOwners { get; set; }

    }
}
