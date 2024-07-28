using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ClientService.API.Entities
{
    public class Client
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TrainerId { get; set; }
        public int CurrentProgramId { get; set; }
        public string ImageFile { get; set; }

    }
}
