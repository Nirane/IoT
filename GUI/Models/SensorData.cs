using System;
using System.Runtime.Serialization;

namespace GUI.Models
{
    [DataContract]
    public class SensorData
    {
        [DataMember(Name = "id")] public string Id { get; set; }

        [DataMember(Name = "sensorId")] public int SensorId { get; set; }

        [DataMember(Name = "type")] public string type { get; set; }

        [DataMember(Name = "date")] public DateTime Date { get; set; }

        [DataMember(Name = "value")] public double Value { get; set; }

        public SensorData(string id, int sensorId, string type, DateTime date, double value)
        {
            this.Id = id;
            this.SensorId = sensorId;
            this.type = type;
            this.Date = date;
            this.Value = value;
        }

        public SensorData()
        {
        }
    }
}