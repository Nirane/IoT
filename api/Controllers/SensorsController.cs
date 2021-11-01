using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Services;
using api.Models;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly SensorService _sensorService;

        public SensorsController(SensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet("humidity")]
        public ActionResult<List<SensorData>> GetHumidityMeasures(int limit)
        {
            return _sensorService.GetByType("humidity",limit);
        }

        [HttpGet("pressure")]
        public ActionResult<List<SensorData>> GetPressureMeasures(int limit)
        {
            return _sensorService.GetByType("pressure", limit);
        }

        [HttpGet("methanol")]
        public ActionResult<List<SensorData>> GetMethanolMeasures(int limit)
        {
            return _sensorService.GetByType("ethylen", limit);
        }

        [HttpGet("temperature")]
        public ActionResult<List<SensorData>> GetTemperatureMesearues(int limit)
        {
            return _sensorService.GetByType("temperature", limit);
        }

    }
}
