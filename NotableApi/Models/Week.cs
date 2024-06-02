using System.Text.Json.Serialization;

namespace OrderLy_API.Models
{
    public class Week
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
