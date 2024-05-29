using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace OrderLy_WPF_Client
{
    class FoodItem
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("Price")]
        public double Price { get; set; }
    }
}
