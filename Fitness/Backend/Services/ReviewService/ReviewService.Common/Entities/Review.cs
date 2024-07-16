using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Common.Entities
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id;
        public string TrainerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
