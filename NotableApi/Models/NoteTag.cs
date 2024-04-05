using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotableApi.Models
{
    public class NoteTag
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;
    }
}
