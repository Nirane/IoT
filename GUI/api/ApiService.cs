using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using GUI.Models;
using Newtonsoft.Json;

namespace GUI.api
{
    public class ApiService
    {
        public List<SensorData> GetTempSensorData()
        {
            var responseBody = _makeHttpGet("http://localhost:5002/sensors/temperature");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;
        }

        public List<SensorData> GetHumiditySensorData()
        {
            var responseBody = _makeHttpGet("http://localhost:5002/sensors/humidity");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;
        }

        public List<SensorData> GetEthylenSensorData()
        {
            var responseBody = _makeHttpGet("http://localhost:5002/sensors/methanol");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;
        }

        public List<SensorData> GetPressureSensorData()
        {
            
            var responseBody = _makeHttpGet("http://localhost:5002/sensors/pressure");

            List<SensorData> parsedList = JsonConvert.DeserializeObject<List<SensorData>>(responseBody);

            return parsedList;
            // var responseBody = _makeHttpGet("https://localhost:5001/rest/json/Ethylen");

            // List<Sensor> parsedList = JsonConvert.DeserializeObject<List<Sensor>>(responseBody);

            // List<SensorData> mock = new List<SensorData>();
            //
            // mock.Add(new SensorData(id: "0", sensorId: 1, sensorType: "Temp", date: DateTime.Now,
            //     value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)));
            //
            // mock.Add(new SensorData(id: "0", sensorId: 1, sensorType: "Temp", date: DateTime.Now.AddDays(1),
            //     value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)));
            //
            //
            // return mock;
        }

        Random random = new Random();
        //
        // public List<List<Sensor>> mock()
        // {
        //     List<List<Sensor>> sensors = new List<List<Sensor>>();
        //     var a = DateTime.Now;
        //     var b = a.AddDays(1);
        //
        //     for (int i = 0; i < 15; i++)
        //     {
        //         sensors.Add(new List<Sensor>()
        //         {
        //             new Sensor(id: "0", sensorId: 1, sensorType: "Temp", date: b.AddDays(1),
        //                 value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
        //             new Sensor(id: "2", sensorId: 2, sensorType: "Temp", date: b.AddDays(1),
        //                 value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)),
        //         });
        //     }
        //
        //     return sensors;
        // }

        public string _makeHttpGet(string url)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    true;

                HttpClient client = new HttpClient(clientHandler);
                Task<HttpResponseMessage> futureResponse = client.GetAsync(url);
                futureResponse.Wait();
                HttpResponseMessage response = futureResponse.Result;

                response.EnsureSuccessStatusCode();

                Task<string> futureString = response.Content.ReadAsStringAsync();
                futureString.Wait();
                return futureString.Result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null;
        }
    }
}