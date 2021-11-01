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
        public List<Sensor> GetTempSensorData()
        {
            var responseBody = _makeHttpGet("https://localhost:5001/rest/json/Pressure");

            List<Sensor> parsedList = JsonConvert.DeserializeObject<List<Sensor>>(responseBody);

            return parsedList;
        }

        public List<Sensor> GetHumiditySensorData()
        {
            var responseBody = _makeHttpGet("https://localhost:5001/rest/json/Pressure");

            List<Sensor> parsedList = JsonConvert.DeserializeObject<List<Sensor>>(responseBody);

            return parsedList;
        }

        public List<Sensor> GetEthylenSensorData()
        {
            var responseBody = _makeHttpGet("https://localhost:5001/rest/json/Pressure");

            List<Sensor> parsedList = JsonConvert.DeserializeObject<List<Sensor>>(responseBody);

            return parsedList;
        }

        public List<Sensor> GetPressureSensorData()
        {
            // var responseBody = _makeHttpGet("https://localhost:5001/rest/json/Ethylen");

            // List<Sensor> parsedList = JsonConvert.DeserializeObject<List<Sensor>>(responseBody);

            List<Sensor> mock = new List<Sensor>();

            mock.Add(new Sensor(id: "0", sensorId: 1, sensorType: "Temp", date: DateTime.Now,
                value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)));

            mock.Add(new Sensor(id: "0", sensorId: 1, sensorType: "Temp", date: DateTime.Now.AddDays(1),
                value: Math.Round(random.NextDouble() * (100 - 1) + 1, 2)));


            return mock;
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