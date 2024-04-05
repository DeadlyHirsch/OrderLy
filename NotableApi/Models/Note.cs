using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotableApi.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public NoteTag[] Tags { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
