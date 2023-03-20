using System.Text.Json.Serialization;

namespace CodeChallenge.Models.DTO
{
    public class Director_DTO
    {
        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("birthdate")]
        public DateTime Birthdate { get; set; }
    }
}
