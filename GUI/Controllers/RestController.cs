using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GUI.api;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApplication.Controllers
{
    [Route("Rest")]
    public class RestController : ControllerBase
    {
        private ApiService _apiService;

        public RestController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [FormatFilter]
        [HttpGet("{format}/{sensor}")]
        public List<Sensor> getData(string format, string sensor)
        {
            List<List<Sensor>> data = new List<List<Sensor>>();

            switch (sensor)
            {
                case "Temperature":
                    data = _apiService.GetTempSensorData();
                    break;
                case "Humidity":
                    data = _apiService.GetHumiditySensorData();
                    break;
                case "Ethylen":
                    data = _apiService.GetEthylenSensorData();
                    break;
                case "Pressure":
                    data = _apiService.GetPressureSensorData();
                    break;
            }

            return data.SelectMany(x => x).ToList();
        }


        [HttpGet("{format}/{sensor}/avg")]
        public List<Sensor> getDataAvg(string sensor)
        {
            List<Sensor> dataPoints = new List<Sensor>();

            Random random = new Random();
            TimeSpan duration = new TimeSpan(1, 0, 0, 0);
            var a = DateTime.Now;
            var b = a.AddDays(1);
            foreach (int num in Enumerable.Range(1, 50))
            {
                b = b.AddDays(1);
                dataPoints.Add(new Sensor(1, "1", b, random.NextDouble() * (100 - 1) + 1));
            }

            return dataPoints;
        }
    }
}