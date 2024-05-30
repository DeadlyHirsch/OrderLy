using System.Text.Json.Serialization;

namespace OrderLy_WPF_Client
{
    class Consumer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("foodItems")]
        public FoodItem[] FoodItems { get; set; }
        [JsonPropertyName("moneyOwed")]
        public double MoneyOwed { get; set; }
        [JsonPropertyName("moneyReturn")]
        public double MoneyReturn { get; set; }
    }
}
