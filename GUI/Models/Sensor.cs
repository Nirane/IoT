using System;
using System.Runtime.Serialization;

namespace GUI.Models
{
    [DataContract]
    public class Sensor
    {
        [DataMember(Name = "id")] public string Id { get; set; }

        [DataMember(Name = "sensorId")] public int SensorId { get; set; }

        [DataMember(Name = "sensorType")] public string SensorType { get; set; }

        [DataMember(Name = "date")] public DateTime Date { get; set; }

        [DataMember(Name = "value")] public double Value { get; set; }

        public Sensor(string id, int sensorId, string sensorType, DateTime date, double value)
        {
            this.Id = id;
            this.SensorId = sensorId;
            this.SensorType = sensorType;
            this.Date = date;
            this.Value = value;
        }

        public Sensor()
        {
        }
    }
}