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
        public List<SensorData> getData(string format, string sensor)
        {
            // List<List<Sensor>> data = new List<List<Sensor>>();

            switch (sensor)
            {
                case "Temperature":
                    return _apiService.GetTempSensorData();

                case "Humidity":
                    return _apiService.GetHumiditySensorData();

                case "Ethylen":
                    return _apiService.GetEthylenSensorData();

                case "Pressure":
                    return _apiService.GetPressureSensorData();
            }

            return null;
            // return data.SelectMany(x => x).ToList();
        }


        [HttpGet("{format}/{sensor}/avg")]
        public List<SensorData> getDataAvg(string sensor)
        {
            List<SensorData> avg = new List<SensorData>();

            switch (sensor)
            {
                case "Temperature":
                    avg = _apiService.GetTempSensorData();
                    break;

                case "Humidity":
                    avg = _apiService.GetHumiditySensorData();
                    break;

                case "Ethylen":
                    avg = _apiService.GetEthylenSensorData();
                    break;

                case "Pressure":
                    avg = _apiService.GetPressureSensorData();
                    break;
            }

            List<IGrouping<DateTime, SensorData>> groupedSensors = avg.GroupBy(e => e.Date).ToList();

            List<SensorData> flattenList = new List<SensorData>();
            foreach (IGrouping<DateTime, SensorData> VARIABLE in groupedSensors)
            {
                flattenList.Add(new SensorData(id: "0", 1, "Temp", VARIABLE.Key, VARIABLE.Average(e => e.Value)));
            }

            return flattenList;
        }
    }
}