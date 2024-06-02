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
        public DateTime? CreatedAt { get; set; }
        [BsonElement("cost")]
        public double? Cost { get; set; }
        [BsonElement("vendor")]
        public Vendor Vendor { get; set; }
        [BsonElement("consumers")]
        public Consumer[] Consumers { get; set; }
    }
}
