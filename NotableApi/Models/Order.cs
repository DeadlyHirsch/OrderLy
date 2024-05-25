using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderLy_API.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now.ToLocalTime();
        [BsonElement("cost")]
        public double? Cost { get; set; } = 0.0;
        [BsonElement("vendor")]
        public Vendor Vendor { get; set; } = null!;
        [BsonElement("consumers")]
        public Consumer[] Consumers { get; set; } = null!;
    }
}
