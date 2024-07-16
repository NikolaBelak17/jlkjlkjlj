using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TrainerService.API.Entities
{
    public class ReviewType
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id;
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
