using System.Text.Json.Serialization;

namespace OrderLy_WPF_Client
{
    class Vendor
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
