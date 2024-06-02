using System.Text.Json.Serialization;

namespace OrderLy_WPF_Client
{
    public class Order
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("cost")]
        public double Cost { get; set; }
        [JsonPropertyName("vendor")]
        public Vendor Vendor { get; set; }
        [JsonPropertyName("consumers")]
        public List<Consumer> Consumers { get; set; }
        [JsonIgnore]
        public int ConsumerCount => Consumers.Count;
    }
}
