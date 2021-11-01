using System;
using System.Collections.Generic;
using GUI.Models;

namespace GUI.api
{
    public class ApiService
    {
        
        public List<List<Sensor>> GetTempSensorData()
        {
            return mock();
        }
        
        public List<List<Sensor>> GetHumiditySensorData()
        {
            return mock();
        }
        
        public List<List<Sensor>> GetEthylenSensorData()
        {
            return mock();
        }
        
        public List<List<Sensor>> GetPressureSensorData()
        {
            return mock();
        }

        Random random = new Random();
        public List<List<Sensor>> mock()
        {
            List<List<Sensor>> sensors = new List<List<Sensor>>();
            var a = DateTime.Now;
            var b = a.AddDays(1);

            for (int i = 0; i < 15; i++)
            {
                sensors.Add(new List<Sensor>()
                {
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
                    new Sensor( sensorId:1 , sensorType: "Temp", date: b.AddDays(1), value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
               
                });
            }

            return sensors;
        }
    }
}