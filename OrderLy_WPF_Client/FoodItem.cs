﻿using System.Text.Json.Serialization;

namespace OrderLy_WPF_Client
{
    public class FoodItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
    }
}
