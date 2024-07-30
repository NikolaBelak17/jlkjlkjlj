using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ClientService.API.Entities
{
    public class ClientSchedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ClientId")]
        public string ClientId { get; set; }
        public List<WeeklySchedule> WeeklySchedules { get; set; } = new List<WeeklySchedule>();
        public ClientSchedule(string id)
        {
            var start_week = 1;
            var end_week = 5;
            ClientId = id;
            for (int i = start_week; i <= start_week; ++i)
            {
                WeeklySchedules.Add(new WeeklySchedule(i));
            }
        }
    }
}
