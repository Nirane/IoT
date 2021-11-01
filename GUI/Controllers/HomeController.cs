using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GUI.api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;


namespace GUI.Controllers
{
    [FormatFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApiService _apiService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            Console.Out.WriteLine(HttpContext.Request.GetDisplayUrl());
            return View();
        }

        public IActionResult License()
        {
            return View();
        }

        public IActionResult TablesEthylen()
        {
            List<string> table = HttpContext.Request.GetDisplayUrl().Split('/').ToList();
            string domain = String.Join('/', table.GetRange(0, 3));
            ViewData.Add("DOMAIN", domain);

            ViewData.Add("NAME", "Ethylen");

            var sensor = _apiService.GetEthylenSensorData();

            return View("Tables", sensor);
        }

        [DisplayName]
        public IActionResult TablesTemp()
        {
            List<string> table = HttpContext.Request.GetDisplayUrl().Split('/').ToList();
            string domain = String.Join('/', table.GetRange(0, 3));
            ViewData.Add("DOMAIN", domain);

            ViewData.Add("NAME", "Temperature");

            var sensor = _apiService.GetTempSensorData();

            return View("Tables", sensor);
        }

        public IActionResult TablesHumidity()
        {
            List<string> table = HttpContext.Request.GetDisplayUrl().Split('/').ToList();
            string domain = String.Join('/', table.GetRange(0, 3));
            ViewData.Add("DOMAIN", domain);

            ViewData.Add("NAME", "Humidity");

            var sensor = _apiService.GetHumiditySensorData();

            return View("Tables", sensor);
        }

        public IActionResult TablesPressure()
        {
            List<string> table = HttpContext.Request.GetDisplayUrl().Split('/').ToList();
            string domain = String.Join('/', table.GetRange(0, 3));
            ViewData.Add("DOMAIN", domain);

            ViewData.Add("NAME", "Pressure");

            var sensor = _apiService.GetPressureSensorData();

            return View("Tables", sensor);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}