using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class SensorDataDto
    {
        public int SensorId { get; set; }

        public string SensorType { get; set; }

        public float Value { get; set; }

        public DateTime Date { get; set; }
    }
}
