using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderLy_WPF_Client
{
    internal class Week
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
