using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GUI.Models;
using Newtonsoft.Json;


namespace GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private List<DataPoint> emp;
        Random random = new Random();

        public HomeController(ILogger<HomeController> logger)
        {    var a = DateTime.Now;
            var b = a.AddDays(1);
            _logger = logger;
            emp = new List<DataPoint>()
            {
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
                new DataPoint(x: b.AddDays(1), y: Math.Round(random.NextDouble() * (100 - 1) + 1,2)),
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public ContentResult GetFirstSensorJSON()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            Random random = new Random();
            TimeSpan duration = new TimeSpan(1, 0, 0, 0);
            var a = DateTime.Now;
            var b = a.AddDays(1);
            foreach (int num in Enumerable.Range(1, 50))
            {
                b = b.AddDays(1);
                dataPoints.Add(new DataPoint(b, random.NextDouble() * (100 - 1) + 1));
                Console.Out.WriteLine(b);
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
                {NullValueHandling = NullValueHandling.Ignore};
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }
        
        [HttpGet("json")]
        public ContentResult GetFirstSensorJSON2()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            Random random = new Random();
            TimeSpan duration = new TimeSpan(1, 0, 0, 0);
            var a = DateTime.Now;
            var b = a.AddDays(1);
            foreach (int num in Enumerable.Range(1, 50))
            {
                b = b.AddDays(1);
                dataPoints.Add(new DataPoint(b, random.NextDouble() * (100 - 1) + 1));
                Console.Out.WriteLine(b);
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings()
                {NullValueHandling = NullValueHandling.Ignore};
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }
        

        public IActionResult License()
        {
            return View();
        }

        public IActionResult Tables()
        {
            return View(emp);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}