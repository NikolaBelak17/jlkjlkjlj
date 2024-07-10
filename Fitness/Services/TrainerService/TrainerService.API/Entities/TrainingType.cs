﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace TrainerService.API.Entities
{
    public class TrainingType
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Difficulty { get; set; }
    }
}
