using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class SensorData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int SensorId { get; set; }

        public string Type { get; set; }

        public float Value { get; set; }

        public DateTime Date { get; set; }
    }
}
