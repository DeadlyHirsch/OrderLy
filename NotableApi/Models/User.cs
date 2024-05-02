using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotableApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Username")]
        public string UserName { get; set; } = null!;

        [BsonElement("Password")]
        public string Password { get; set; } = null!;
    }
}
