using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderLy_API.Models
{
    public class Vendor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("vendor_name")]
        public string Name { get; set; } = null!;
    }
}
