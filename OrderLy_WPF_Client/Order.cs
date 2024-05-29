using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderLy_WPF_Client
{
    class Order
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("cost")]
        public double Cost { get; set; }
        [JsonPropertyName("vendor")]
        public Vendor Vendor { get; set; } = null!;
        [JsonPropertyName("consumers")]
        public Consumer[] Consumers { get; set; } = null!;
    }
}
