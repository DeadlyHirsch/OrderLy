using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace OrderLy_WPF_Client
{
    class Vendor
    {
        [JsonPropertyName("_id")]
        public string ID { get; set; } = null!;
        [JsonPropertyName("vendor_name")]
        public string Name { get; set; } = null!;
    }
}
