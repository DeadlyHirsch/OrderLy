using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderLy_WPF_Client
{
    class Consumer
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("FoodItems")]
        public FoodItem[] FoodItems { get; set; } = null!;
        [JsonPropertyName("MoneyOwed")]
        public double MoneyOwed { get; set; }
        [JsonPropertyName("MoneyReturn")]
        public double MoneyReturn { get; set; } 
    }
}
