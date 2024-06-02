using System.Text.Json.Serialization;

namespace OrderLy_WPF_Client
{
    public class Vendor
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
