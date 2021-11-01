using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GUI.Models
{
    [DataContract]
    public class Sensor
    {
        [DataMember(Name = "sensorId")] public int SensorId { get; set; }

        [DataMember(Name = "sensorType")] public string SensorType { get; set; }

        [DataMember(Name = "date")] public DateTime Date { get; set; }
        
        [DataMember(Name = "value")] public double Value { get; set; }

        public Sensor(int sensorId, string sensorType, DateTime date, double value)
        {
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